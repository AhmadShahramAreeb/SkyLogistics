using System.Linq;
using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

/*
 * ## 1. The Big Picture
   We are using a **Generic Repository Pattern**.
   **Generic**: We write the common code (Create, Read, Update, Delete) once and reuse it for all tables (Drone, Users, Products, etc.).
   **Specific**: We create specific repositories (like `DroneRepository`) for logic that only applies to that specific table.

 **Problem**: `RepositoryBase` is too generic. It doesn't know about "Drone".
 **Solution**: We create a specific repository for Drone that inherits the generic powers but can also have specific rules.
 */
public class DroneRepository:RepositoryBase<Drone>, IDroneRepository
{
  public DroneRepository(RepositoryContext context) : base(context)
  {
  }


  public void CreateOneDrone(Drone drone) => Create(drone);

  public void UpdateOneDrone(Drone drone) => Update(drone);

  public void DeleteOneDrone(Drone drone)=>Delete(drone);

  public IQueryable<Drone> GetAllDrones(bool trackChanges) =>
    FindAll(trackChanges).OrderBy(d => d.Id);

  public IQueryable<Drone> GetAllDronesByBatteryLevel(bool trackChanges) =>
    FindAll(trackChanges).OrderByDescending(d => d.BatteryLevel);

  public Drone GetOneDroneById(int id, bool trackChanges) =>
    FindByCondition(d => d.Id.Equals(id), trackChanges).SingleOrDefault();
}