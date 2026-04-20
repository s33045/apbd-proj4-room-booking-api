using RoomBookingApi.Models;

namespace RoomBookingApi.Repositories;

public class RoomRepository : IRoomRepository
{
    private static int _nextId = 1;
    private static readonly List<Room> _rooms = [];

    public IEnumerable<Room> GetAll()
    {
        return _rooms;
    }

    public Room? GetById(int id)
    {
        return _rooms.FirstOrDefault(room => room.Id == id);
    }

    public IEnumerable<Room> GetByBuildingCode(string buildingCode)
    {
        return _rooms.Where(room => room.BuildingCode == buildingCode);
    }

    public IEnumerable<Room> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var query = _rooms.AsEnumerable();

        if (minCapacity.HasValue)
            query = query.Where(room => room.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            query = query.Where(room => room.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            query = query.Where(room => room.IsActive);

        return query;
    }

    public void Add(Room room)
    {
        room.Id = _nextId++;
        _rooms.Add(room);
    }

    public bool Update(Room room)
    {
        var existing = GetById(room.Id);
        if (existing == null) return false;

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return true;
    }

    public bool Delete(int id)
    {
        var room = _rooms.FirstOrDefault(room => room.Id == id);
        if (room == null) return false;
        _rooms.Remove(room);
        return true;
    }

    public bool Exists(int id)
    {
        return _rooms.Any(room => room.Id == id);
    }
}