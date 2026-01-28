using System.Linq;
using Entities.Models;

namespace Repositories.Contracts;
/*
    PURPOSE: Specific methods for Drone operations.
   AMAÇ   : Drone işlemleri için özel metotlar.
   */
public interface IDroneRepository:IRepositoryBase<Drone>
{

    /*
## 1. It means `IBookRepository` **automatically has** all the methods from `IRepositoryBase`.
They are "invisible" but they are there.
*/

    /*
## 2. So why did we add `GetAllBooks`? (The Wrapper)

     If we already have `FindAll`, why create `GetAllBooks`?

### Reason A: To Add Default Logic (Sorting)
     Look at the implementation in `BookRepository.cs`:

         ```csharp
// Implementation
     public IQueryable<Book> GetAllBooks(bool trackChanges) =>
         FindAll(trackChanges).OrderBy(b => b.Id); // <--- LOOK HERE!
         ```

     *   **`FindAll`**: Just gives you the raw list. Unsorted.
     *   **`GetAllBooks`**: Gives you the list **Ordered by ID**.

     If we just used `FindAll` in the Controller, the Controller would have to remember to sort it every time:
         ```csharp
// Bad Way (Controller doing work)
     _repository.FindAll(false).OrderBy(b => b.Id).ToList();
         ```

     By creating `GetAllBooks`, we hide that logic inside the Repository. The Controller just says "Give me books" and trusts they are sorted correctly.

### Reason B: To Simplify Complex Queries
         Look at `GetOneBookById`:

         ```csharp
// Implementation
     public IQueryable<Book> GetOneBookById(int id, bool trackChanges) =>
         FindByCondition(b => b.Id.Equals(id), trackChanges);
         ```

     *   **`FindByCondition`**: Requires you to write a Lambda Expression (`b => b.Id == id`).
     *   **`GetOneBookById`**: Just asks for an `int id`.

     It is much easier for the Controller to call `GetOneBookById(5)` than to write `FindByCondition(x => x.Id == 5)`.
     */

    /*
 * If you didn't create GetAllBooks, you would have to write .OrderBy(b => b.Id) inside your Controller every single time.
   By creating GetAllBooks, you hide that rule inside the Repository, keeping your Controller clean.
 */

    IQueryable<Drone> GetAllDrones(bool trackChanges);
    Drone GetOneDroneById(int id, bool trackChanges);
    void CreateOneDrone(Drone drone);
    void UpdateOneDrone(Drone drone);
    void DeleteOneDrone(Drone drone);

}