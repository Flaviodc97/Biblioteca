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
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _notificationRepository = unitOfWork.GetRepository<Notification>();
        }
        public async Task<NotificationDTO> AddAsync(NotificationDTO dto)
        {
            try
            {
                var notification = await _notificationRepository.AddAsync(_mapper.Map<Notification>(dto));
                if (notification is null) throw new Exception("Notification not Created");
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<NotificationDTO>(notification);

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

        public async Task<bool> Delete(int id)
        {
            try
            {
                var notification = await _notificationRepository.GetByIdAsync(id);
                var result = _notificationRepository.Remove(notification);
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

        public async Task<List<NotificationDTO>> GetAllAsync()
        {
            try
            {
                var notificationList =await  _notificationRepository.GetAllAsync();
                if (!notificationList.Any()) throw new Exception("No Notification Found");
                return _mapper.Map<List<NotificationDTO>>(notificationList);

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

        public async Task<NotificationDTO> GetAsync(int id)
        {
            try
            {
                var notification = await _notificationRepository.GetByIdAsync(id);
                if (notification is null) throw new Exception($"No Notification with id {id} found!");
                return _mapper.Map<NotificationDTO>(notification);
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

        public async Task<NotificationDTO> UpdateAsync(NotificationDTO dto)
        {
            try
            {
                var result = await _notificationRepository.UpdateAsync(_mapper.Map<Notification>(dto));
                await _unitOfWork.SaveChangesAsync();
                if (result is null) throw new Exception("Error during the update");
                return _mapper.Map<NotificationDTO>(result);
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
