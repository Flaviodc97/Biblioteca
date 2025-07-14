using AutoMapper;
using BibliotecaBLL.DTOs;
using BibliotecaBLL.IServices;
using BibliotecaDAL.Entities;
using BibliotecaDAL.IRepositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.Serivices
{
    public class PublisherSerivice : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Publisher> _publishRepository;
        private readonly IMapper _mapper;
        public PublisherSerivice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _publishRepository = _unitOfWork.GetRepository<Publisher>();
            _mapper = mapper;
        }
        public async Task<PublisherDTO> AddAsync(PublisherDTO dto)
        {
            try
            {
                var publisher = await _publishRepository.AddAsync(_mapper.Map<Publisher>(dto));

                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<PublisherDTO>(publisher);
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
                var publisher = await _publishRepository.GetByIdAsync(id);

                var result = _publishRepository.Remove(publisher);

                await _unitOfWork.SaveChangesAsync();

                return result;

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

        public async Task<List<PublisherDTO>> GetAllAsync()
        {
            try
            {
                var publisherList = await _publishRepository.GetAllAsync();

                return _mapper.Map<List<PublisherDTO>>(publisherList);
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

        public async Task<PublisherDTO> GetAsync(int id)
        {
            try
            {
                var publisher = await _publishRepository.GetByIdAsync(id);
                return _mapper.Map<PublisherDTO>(publisher);
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

        public async Task<PublisherDTO> UpdateAsync(PublisherDTO dto)
        {
            try
            {
                var publisher = await _publishRepository.UpdateAsync(_mapper.Map<Publisher>(dto));

                return _mapper.Map<PublisherDTO>(publisher);

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
    }
}
