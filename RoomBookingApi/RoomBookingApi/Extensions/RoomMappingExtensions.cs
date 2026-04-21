using RoomBookingApi.DTOs;
using RoomBookingApi.Models;

namespace RoomBookingApi.Extensions;

public static class RoomMappingExtensions
{
    public static Room ToDomain(this CreateRoomDto dto)
    {
        return new Room
        {
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = dto.IsActive
        };
    }

    public static Room ToDomain(this UpdateRoomDto dto, int id)
    {
        return new Room
        {
            Id = id,
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = dto.IsActive
        };
    }

    public static RoomDto ToDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        };
    }
}