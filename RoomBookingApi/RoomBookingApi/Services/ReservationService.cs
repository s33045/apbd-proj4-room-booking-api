using RoomBookingApi.Exceptions;
using RoomBookingApi.Models;
using RoomBookingApi.Models.Enums;
using RoomBookingApi.Repositories;

namespace RoomBookingApi.Services;

public class ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
    : IReservationService
{
    public IEnumerable<Reservation> GetAll()
    {
        return reservationRepository.GetAll();
    }

    public Reservation? GetById(int id)
    {
        return reservationRepository.GetById(id) ?? throw new ReservationNotFoundException(id);
    }

    public IEnumerable<Reservation> GetFiltered(DateOnly? date, ReservationStatus? status, int? roomId)
    {
        return reservationRepository.GetFiltered(date, status, roomId);
    }

    public bool Add(Reservation reservation)
    {
        if (!roomRepository.Exists(reservation.RoomId))
            throw new RoomNotFoundException(reservation.RoomId);

        reservationRepository.Add(reservation);
        return true;
    }

    public bool Update(Reservation reservation)
    {
        if (!reservationRepository.Exists(reservation.Id))
            throw new ReservationNotFoundException(reservation.Id);

        if (!roomRepository.Exists(reservation.RoomId))
            throw new RoomNotFoundException(reservation.RoomId);

        return reservationRepository.Update(reservation);
    }

    public bool Delete(int id)
    {
        if (!reservationRepository.Exists(id))
            throw new ReservationNotFoundException(id);

        return reservationRepository.Delete(id);
    }

    public bool HasConflict(int roomId, DateOnly date, TimeOnly startTime, TimeOnly endTime,
        int? ignoredReservationId = null)
    {
        return reservationRepository
            .GetFiltered(date, null, roomId)
            .Where(reservation => reservation.Id != ignoredReservationId)
            .Where(reservation => reservation.Status != ReservationStatus.Cancelled)
            .Any(reservation => startTime < reservation.EndTime && endTime > reservation.StartTime);
    }

    public bool AnyForRoom(int roomId)
    {
        return reservationRepository.AnyForRoom(roomId);
    }
}