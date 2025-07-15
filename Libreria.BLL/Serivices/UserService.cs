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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>();
            _mapper = mapper;
        }
        public async Task<UserDTO> AddAsync(UserDTO dto)
        {
            try
            {
                var user = await _userRepository.AddAsync(_mapper.Map<User>(dto));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<UserDTO>(user);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error; ", ex);
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
                var userToDelete = await _userRepository.GetByIdAsync(id);
                var result = _userRepository.Remove(userToDelete);
                await _unitOfWork.SaveChangesAsync();
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error; ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            try
            {
                var userList = await _userRepository.GetAllAsync();
                return _mapper.Map<List<UserDTO>>(userList);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error; ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null) throw new Exception($"User not Found with id: {id}");
                return _mapper.Map<UserDTO>(user);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error; ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> UpdateAsync(UserDTO dto)
        {
            try
            {
                var res = await _userRepository.UpdateAsync(_mapper.Map<User>(dto));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<UserDTO>(dto);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error; ", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
