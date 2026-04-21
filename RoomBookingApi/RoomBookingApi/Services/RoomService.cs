using RoomBookingApi.Exceptions;
using RoomBookingApi.Models;
using RoomBookingApi.Repositories;

namespace RoomBookingApi.Services;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
    public IEnumerable<Room> GetAll()
    {
        return roomRepository.GetAll();
    }

    public Room? GetById(int id)
    {
        return roomRepository.GetById(id)
               ?? throw new RoomNotFoundException(id);
    }

    public IEnumerable<Room> GetByBuildingCode(string buildingCode)
    {
        return roomRepository.GetByBuildingCode(buildingCode);
    }

    public IEnumerable<Room> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        return roomRepository.GetFiltered(minCapacity, hasProjector, activeOnly);
    }

    public void Add(Room room)
    {
        roomRepository.Add(room);
    }

    public bool Update(Room room)
    {
        if (!roomRepository.Exists(room.Id))
            throw new RoomNotFoundException(room.Id);

        return roomRepository.Update(room);
    }

    public bool Delete(int id)
    {
        if (!roomRepository.Exists(id))
            throw new RoomNotFoundException(id);

        return roomRepository.Delete(id);
    }
}