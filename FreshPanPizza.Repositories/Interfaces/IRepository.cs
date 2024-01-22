using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshPanPizza.Repositories.Interfaces
{
    //Generic Repository.
    //1. TEntity: This is my generic interface where I define a TEntity because we`re going to pass a Entity class.
    //2. <TEntity> is a type & TEntity is a class.
    //3. This is a C# generic, a way how we define the interface in similar way we`ll define the class.
    //4. In this current interface base on this TEntity class we`ll define the various methods (Add, Update, Remove & Delete).
    public interface IRepository<TEntity> where TEntity : class
    {
        //1. Defining method to get the list of Entities. This GetAll() method will return the list of Entities.
        //2. For ex: If you will pass TEntity as Item Entity it`ll return the Item list or Order Entity it`ll return the Order list.
        IEnumerable<TEntity> GetAll();

        //1. Returning the single Entity based upon the Primary key.
        //2. Passing Primary key as "id" & datatype as "object" in parameter of Find().
        //3. Why datatype m using as object because the primary key can be integer, string or else so object is a base class in C#, So 
        //   object can accept any type of data so for primary key m using as object.
        //4. Defining generic class/interface, we talk it about the Generic way so that the methods, parameter can works with the different,
        //   types of datatypes & with different types of classes, so that`s why m using object type.
        TEntity Find(object id);

        //Define to add a method for new Entity.
        void Add(TEntity entity);

        //Define to update a method for existing Entity.
        void Update(TEntity entity);

        //Define to remove anything from exisiting Entity.
        void Remove(TEntity entity);

        //Define to delete the entity.
        void Delete(object Id);

        //we need to commit these above operations into the database for that, will define SaveChange() method.
        int SaveChanges();

        //So now, interface will implementated in class (Repository) -> `FreshPanPizza.Repositories -> Implementations -> Repository`
    }
}
