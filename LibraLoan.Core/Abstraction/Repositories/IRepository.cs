using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Core.Abstraction.Repositories
{
    public interface IRepository<TModel, TModelDto>
    {
        Task<IEnumerable<TModel>> GetAllAsync(bool reload);
        Task<TModel> CreateAsync(TModelDto model);
        Task<bool> DeleteAsync(TModel model);
        Task<bool> UpdateAsync(TModelDto model);
    }
}
