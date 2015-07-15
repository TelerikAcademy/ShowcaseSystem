namespace Showcase.Data.Common.Repositories
{
    using System.Linq;

    using Showcase.Data.Common.Models;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class, IDeletableEntity
    {
        IQueryable<T> AllWithDeleted();

        void ActualDelete(T entity);
    }
}
