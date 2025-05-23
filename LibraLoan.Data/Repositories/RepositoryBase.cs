using LibraLoan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LibraLoan.Data.Repositories
{
    internal abstract class RepositoryBase<TModel> where TModel : ModelBase
    {
        public RepositoryBase(IAppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public abstract Task<IEnumerable<TModel>> GetAllAsync(bool reload);

        public virtual async Task<bool> DeleteAsync(TModel model)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                TModel dbModel = await dbContext.Set<TModel>().FindAsync(model.Id);
                dbContext.Set<TModel>().Remove(dbModel);
                try
                {
                    dbContext.SaveChanges();
                    _models.Remove(model);
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        protected readonly IAppDbContextFactory _dbContextFactory;

        protected IEnumerable<TModel> SetModels(IEnumerable<TModel> models)
        {
            _models = new ObservableCollection<TModel>(models);
            _isLoaded = true;
            return _models;
        }

        protected bool _isLoaded;
        protected ObservableCollection<TModel> _models;

    }
}
