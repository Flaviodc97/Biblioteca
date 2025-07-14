using BibliotecaBLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.IServices
{
    public interface IService <T> where T : class
    {
        Task<T> AddAsync(T dto);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<bool> Delete(int id);
        Task<T> UpdateAsync(T dto);
    }
}
