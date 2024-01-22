using FreshPanPizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshPanPizza.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //1. We need to perform the Database operation on entities using AppDbContext.
        //2. DbContext class helps us to maintain the entity life cycle & which is helping us to peform the CRUD operation on DB Table.
        //3. I`m going to use a class which can help me in a Generic way to perform the Database operation on any entity.
        //4. I`ll create a instance of DbContext class. Making protected so that I can access in my derive class.
        //5. This `DbContext` class help us to peform the CRUD operation on entity.

        protected DbContext _dbContext; //Constructor injection

        //Initialized the DbContext
        public Repository(DbContext dbContext)  //So pass dbContext using the Dependency Injection
        {
            _dbContext = dbContext;
        }
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(object Id)
        {
            TEntity entity = _dbContext.Set<TEntity>().Find(Id);
            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);   
            }
        }

        public TEntity Find(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
           return _dbContext.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
           _dbContext.Set<TEntity>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
