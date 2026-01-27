using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore;
/*
 ### `RepositoryBase.cs` (The Generic Worker)
   *   **Type**: Abstract Class (Implements `IRepositoryBase<T>`)
   *   **Role**: The actual code that does the work defined in the interface.
   *    Why do we need it? : To avoid repeating code (DRY Principle).
   *   Without this*: You would write the same `_context.Set<Drone>().Add(entity)` code in
   `DroneRepository`, `UserRepository`, etc.
   *   With this*: You write it once here, and everyone inherits it.
   *   *How it works**: It takes the `RepositoryContext` in its constructor and uses it to
   perform database operations.
 */

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class

{
    protected readonly RepositoryContext _context;

    protected RepositoryBase(RepositoryContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
        //  _context.Set<T>(); Bu metod değişlikleri yapmak için çalışacak
        !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();


    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges) =>
        !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);
}