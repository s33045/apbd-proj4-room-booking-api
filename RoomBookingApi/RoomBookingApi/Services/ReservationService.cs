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
        return reservationRepository.GetById(id);
    }

    public IEnumerable<Reservation> GetFiltered(DateOnly? date, ReservationStatus? status, int? roomId)
    {
        return reservationRepository.GetFiltered(date, status, roomId);
    }

    public bool Add(Reservation reservation)
    {
        if (!roomRepository.Exists(reservation.RoomId))
            return false;

        if (reservation.StartTime >= reservation.EndTime)
            return false;

        reservationRepository.Add(reservation);
        return true;
    }

    public bool Update(Reservation reservation)
    {
        if (!roomRepository.Exists(reservation.RoomId))
            return false;

        if (reservation.StartTime >= reservation.EndTime)
            return false;

        return reservationRepository.Update(reservation);
    }

    public bool Delete(int id)
    {
        return reservationRepository.Delete(id);
    }
}