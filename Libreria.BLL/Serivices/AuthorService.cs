using AutoMapper;
using BibliotecaBLL.DTOs;
using BibliotecaBLL.IServices;
using BibliotecaDAL.Entities;
using BibliotecaDAL.IRepositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _authorRepository = _unitOfWork.GetRepository<Author>();
        }

        public async Task<AuthorDTO> AddAsync(AuthorDTO dto)
        {
            try
            {
                var author = await _authorRepository.AddAsync(_mapper.Map<Author>(dto));
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

    }
}
