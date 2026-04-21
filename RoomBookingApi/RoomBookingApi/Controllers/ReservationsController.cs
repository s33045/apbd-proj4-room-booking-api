using Microsoft.AspNetCore.Mvc;
using RoomBookingApi.DTOs;
using RoomBookingApi.Exceptions;
using RoomBookingApi.Extensions;
using RoomBookingApi.Models.Enums;
using RoomBookingApi.Services;

namespace RoomBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController(IReservationService reservationService, IRoomService roomService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] ReservationStatus? status,
        [FromQuery] int? roomId)
    {
        var reservations = date.HasValue || status.HasValue || roomId.HasValue
            ? reservationService.GetFiltered(date, status, roomId)
            : reservationService.GetAll();

        return Ok(reservations.Select(reservation => reservation.ToDto()));
    }

    [HttpGet("{id:int}")]
    public ActionResult<ReservationDto> GetById(int id)
    {
        try
        {
            var reservation = reservationService.GetById(id);
            return Ok(reservation!.ToDto());
        }
        catch (ReservationNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<ReservationDto> Create(CreateReservationDto dto)
    {
        try
        {
            var room = roomService.GetById(dto.RoomId);

            if (!room!.IsActive)
                return Conflict("Cannot create reservation for inactive room.");

            if (reservationService.HasConflict(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime))
                return Conflict("Reservation conflicts with an existing reservation.");

            var reservation = dto.ToDomain();
            reservationService.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation.ToDto());
        }
        catch (RoomNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<ReservationDto> Update(int id, UpdateReservationDto dto)
    {
        try
        {
            reservationService.GetById(id);

            var room = roomService.GetById(dto.RoomId);

            if (!room!.IsActive)
                return Conflict("Cannot update reservation for inactive room.");

            if (reservationService.HasConflict(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime, id))
                return Conflict("Reservation conflicts with an existing reservation.");

            var reservation = dto.ToDomain(id);
            reservationService.Update(reservation);

            return Ok(reservation.ToDto());
        }
        catch (ReservationNotFoundException ex)
        {
            return NotFound(ex.Message);
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
            reservationService.Delete(id);
            return NoContent();
        }
        catch (ReservationNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}