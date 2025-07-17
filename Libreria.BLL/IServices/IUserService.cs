using BibliotecaBLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.IServices
{
    public interface IUserService : IService<UserDTO>
    {
        Task<PaginatedListDTO<UserDTO>> GetUSersPaginated(int pageIndex, int pageSize);
    }
}
