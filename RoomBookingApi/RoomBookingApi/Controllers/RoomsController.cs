using Microsoft.AspNetCore.Mvc;
using RoomBookingApi.DTOs;
using RoomBookingApi.Exceptions;
using RoomBookingApi.Extensions;
using RoomBookingApi.Services;

namespace RoomBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController(IRoomService roomService, IReservationService reservationService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<RoomDto>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = minCapacity.HasValue || hasProjector.HasValue || activeOnly.HasValue
            ? roomService.GetFiltered(minCapacity, hasProjector, activeOnly)
            : roomService.GetAll();

        return Ok(rooms.Select(room => room.ToDto()));
    }

    [HttpGet("{id:int}")]
    public ActionResult<RoomDto> GetById(int id)
    {
        try
        {
            var room = roomService.GetById(id);
            return Ok(room!.ToDto());
        }
        catch (RoomNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<RoomDto>> GetByBuildingCode(string buildingCode)
    {
        var rooms = roomService.GetByBuildingCode(buildingCode);
        return Ok(rooms.Select(room => room.ToDto()));
    }

    [HttpPost]
    public ActionResult<RoomDto> Create(CreateRoomDto dto)
    {
        var room = dto.ToDomain();
        roomService.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room.ToDto());
    }

    [HttpPut("{id:int}")]
    public ActionResult<RoomDto> Update(int id, UpdateRoomDto dto)
    {
        try
        {
            var room = dto.ToDomain(id);
            roomService.Update(room);
            return Ok(room.ToDto());
        }
        catch (RoomNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            roomService.GetById(id);

            if (reservationService.AnyForRoom(id))
                return Conflict("Cannot delete room with existing reservations.");

            roomService.Delete(id);
            return NoContent();
        }
        catch (RoomNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}