using AutoMapper;
using BibliotecaBLL.DTOs;
using BibliotecaBLL.Exceptions;
using BibliotecaBLL.IServices;
using BibliotecaDAL.Entities;
using BibliotecaDAL.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.Serivices
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _bookRepository = unitOfWork.GetRepository<Book>();
            _mapper = mapper;
        }
        public async Task<BookDTO> AddAsync(BookDTO bookDTO)
        {
            try
            {
                var book = await _bookRepository.AddAsync(_mapper.Map<Book>(bookDTO));
                await _unitOfWork.SaveChangesAsync();
                if (book is null) throw new BookNotFoundException($"Book not Added {bookDTO.Title}");
                return _mapper.Map<BookDTO>(book);

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

        public async Task<bool> Delete(int id)
        {
            try
            {

                var bookToDelete = await _bookRepository.GetByIdAsync(id);

                if (bookToDelete is null) throw new BookNotFoundException($"No Book with {id} found!");

                var result = _bookRepository.Remove(bookToDelete);
                await _unitOfWork.SaveChangesAsync();

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

        public async Task<BookDTO> GetAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id); 

                if (book is null) throw new BookNotFoundException($"Book with {id}, not found");

                return _mapper.Map<BookDTO>(book);

            }
            catch (SqlException ex)
            {
               
                throw new Exception("Database error: ", ex);
            
            }
            catch (Exception ex)
            {
                throw;            
            }
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            try
            {
                var booksList = await _bookRepository.GetAllAsync();
                if (booksList.Count() == 0) throw new BookNotFoundException("No Books Found");
                return _mapper.Map<List<BookDTO>>(booksList);
            }
            catch (SqlException ex)
            {

                throw new Exception("Database error: ", ex);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BookDTO> UpdateAsync(BookDTO dto)
        {
            try
            {
                var book = await _bookRepository.UpdateAsync(_mapper.Map<Book>(dto));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<BookDTO>(book);

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
    }
}
