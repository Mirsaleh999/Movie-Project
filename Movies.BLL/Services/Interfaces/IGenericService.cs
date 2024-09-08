using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Services.Interfaces
{
    public interface IGenericService<TDto, TEntity> where TDto : class where TEntity : class
    {
        public Task<TDto> AddAsync(TDto item);
        public Task<TDto> GetByIdAsync(int id);
        public Task<List<TDto>> GetListAsync();
        public void Delete(int id);
        public TDto Update(TDto item);

    }
}
