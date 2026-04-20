using RoomBookingApi.Models;

namespace RoomBookingApi.Services;

public interface IRoomService
{
    IEnumerable<Room> GetAll();
    Room? GetById(int id);
    IEnumerable<Room> GetByBuildingCode(string buildingCode);
    IEnumerable<Room> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly);
    void Add(Room room);
    bool Update(Room room);
    bool Delete(int id);
}