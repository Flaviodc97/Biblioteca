using AutoMapper;
using BibliotecaBLL.DTOs;
using BibliotecaBLL.DTOs.AuthorDTOS;
using BibliotecaBLL.IServices;
using BibliotecaDAL.Entities;
using BibliotecaDAL.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibliotecaBLL.Serivices
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Author> _authorRepository;
        
        public AuthorService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authorRepository = unitOfWork.GetRepository<Author>();
        }

        public async Task<AuthorDTO> AddAsync(AuthorDTO dto)
        {
            try
            {
                var author = await _authorRepository.AddAsync(_mapper.Map<Author>(dto));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<AuthorDTO>(author);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database: ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var authorToDelete = await _authorRepository.GetByIdAsync(id);
                if (authorToDelete is null) throw new Exception($"Author with id: {id} not found");
                var result = _authorRepository.Remove(authorToDelete);
                if(result) await _unitOfWork.SaveChangesAsync();
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<AuthorDTO>> GetAllAsync()
        {
            try
            {
                var authorList = await _authorRepository.GetAllAsync();
                if(authorList.Count() == 0) throw new Exception("No Author Found");
                return _mapper.Map<List<AuthorDTO>>(authorList);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AuthorDTO> GetAsync(int id)
        {
            try 
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author is null) throw new Exception($"No Author Found with id: {id}");
                return _mapper.Map<AuthorDTO>(author);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthorDTO> UpdateAsync(AuthorDTO dto)
        {
            try
            {
                var result = await _authorRepository.UpdateAsync(_mapper.Map<Author>(dto));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<AuthorDTO>(result);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<AuthorWithBooksDTO> AddBookToAuthorAsync(BookAuthorDTO dto)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
                if (author is null) throw new Exception($"Author with id: {dto.AuthorId} not found!");

                var books = await _unitOfWork.GetRepository<Book>().GetByIdRangeAsync(dto.BookIds);
                if (books.Count() == 0) throw new Exception("No Books found whith the given Ids");
                if (books.Count() != dto.BookIds.Count()) throw new Exception("Error not found all the Books with the given Ids");
                
                foreach (var book in books)
                {
                    // if Author has already this book ignore the Add
                    if(!author.Books.Any(b => b.Id == book.Id))
                        author.Books.Add(book);
                }

                if (!(await _unitOfWork.SaveChangesAsync() > 0)) throw new Exception("Error during the DB Saving");
                return _mapper.Map<AuthorWithBooksDTO>(author);


            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AuthorWithBooksDTO> GetAuthorWithBooks(int id)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetAuthorWithBooks(id);
                return _mapper.Map<AuthorWithBooksDTO>(author);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<AuthorWithBooksDTO> RemoveBookToAuthorAsync(BookAuthorDTO dto)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetAuthorWithBooks(dto.AuthorId);
                if (author is null) 
                    throw new Exception($"Author with id: {dto.AuthorId} not found");
                
                var books = author.Books
                    .Where(x => dto.BookIds.Contains(x.Id))
                    .ToList();

                if (books.Count() == 0) 
                    throw new Exception("No Books found whith the given Ids");

                foreach (var book in books)
                {
                    author.Books.Remove(book);
                }

                if (!(await _unitOfWork.SaveChangesAsync() > 0)) 
                    throw new Exception("Error During Entities Saving");

                return _mapper.Map<AuthorWithBooksDTO>(author);
            }
            catch (SqlException ex) 
            {
                throw new Exception("Database Error:", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaginatedListDTO<AuthorDTO>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var (authorList, totalPages) = await _unitOfWork.AuthorRepository.GetAuthorPaginatedAsync(pageIndex, pageSize);
                var authorListDTO = _mapper.Map<List<AuthorDTO>>(authorList);
                return new PaginatedListDTO<AuthorDTO>(authorListDTO,pageIndex, totalPages);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<AuthorDTO> SearchAuthorList(AuthorSeachDTO authorSeachDTO)
        {
            try
            {
                var query =  _authorRepository.GetQueryable();
                if (!string.IsNullOrEmpty(authorSeachDTO.Name))
                {
                    query = query.Where(p => p.Name.Contains(authorSeachDTO.Name));
                }

                if (!string.IsNullOrEmpty(authorSeachDTO.LastName))
                {
                    query = query.Where(p => p.LastName.Contains(authorSeachDTO.LastName));
                }

                if(authorSeachDTO.StartDateOfBirth.HasValue)
                {
                    query = query.Where(p => p.DateOfBirth >= authorSeachDTO.StartDateOfBirth);
                }

                if (authorSeachDTO.EndDateOfBirth.HasValue)
                {
                    query = query.Where(p => p.DateOfBirth <= authorSeachDTO.EndDateOfBirth);
                }

                if (authorSeachDTO.StartDateOfDeath.HasValue)
                {
                    query = query.Where(p => p.DateOfDeath >= authorSeachDTO.StartDateOfDeath);
                }

                if (authorSeachDTO.EndDateOfDeath.HasValue)
                {
                    query = query.Where(p => p.DateOfDeath <= authorSeachDTO.EndDateOfDeath);
                }

                if (!string.IsNullOrEmpty(authorSeachDTO.Nationality))
                {
                    query = query.Where(p => p.Nationality.Contains(authorSeachDTO.Nationality));
                }

                return _mapper.Map<List<AuthorDTO>>(query.ToList());
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AuthorDTO> GenericSearch(Dictionary<string, object> parameters)
        {
            try
            {
                var query = _unitOfWork.GetRepository<Author>().GetQueryable();

                foreach (var param in parameters)
                {
                    query = SearchService<Author>.GenericSearch(query, param);

                }

                var result = query.ToList();
                return _mapper.Map<List<AuthorDTO>>(result);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error: ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool IsNumericType(object obj)
        {
            if (obj == null) return false;

            // Gestisce JsonElement (System.Text.Json)
            if (obj is JsonElement jsonElement)
            {
                return jsonElement.ValueKind == JsonValueKind.Number;
            }

            // Gestisce tipi numerici standard
            Type type = obj.GetType();
            Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            return underlyingType == typeof(byte) || underlyingType == typeof(sbyte) ||
                   underlyingType == typeof(short) || underlyingType == typeof(ushort) ||
                   underlyingType == typeof(int) || underlyingType == typeof(uint) ||
                   underlyingType == typeof(long) || underlyingType == typeof(ulong) ||
                   underlyingType == typeof(float) || underlyingType == typeof(double) ||
                   underlyingType == typeof(decimal);
        }

    }
}
