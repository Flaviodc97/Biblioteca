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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.GetRepository<Category>();
            _mapper = mapper;
        }
        public async Task<CategoryDTO> AddAsync(CategoryDTO dto)
        {
            try
            {
                var category = await _categoryRepository.AddAsync(_mapper.Map<Category>(dto));
                return _mapper.Map<CategoryDTO>(category);
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
                var categoryToDelete = await _categoryRepository.GetByIdAsync(id);
                var result = _categoryRepository.Remove(categoryToDelete);
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

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            try
            {
                var categoryList = await _categoryRepository.GetAllAsync();
                if (categoryList.Count() == 0) throw new Exception("No Categories Found!");
                return _mapper.Map<List<CategoryDTO>>(categoryList);
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

        public async Task<CategoryDTO> GetAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category is null) throw new Exception($"No Category Found with id: {id}");
                return _mapper.Map<CategoryDTO>(category);
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

        public async Task<CategoryDTO> UpdateAsync(CategoryDTO dto)
        {
            try
            {
                var category = await _categoryRepository.UpdateAsync(_mapper.Map<Category>(dto));
                return _mapper.Map<CategoryDTO>(category);
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
