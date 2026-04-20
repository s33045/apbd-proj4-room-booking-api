using RoomBookingApi.Models;
using RoomBookingApi.Models.Enums;

namespace RoomBookingApi.Services;

public interface IReservationService
{
    IEnumerable<Reservation> GetAll();
    Reservation? GetById(int id);
    IEnumerable<Reservation> GetFiltered(DateOnly? date, ReservationStatus? status, int? roomId);
    bool Add(Reservation reservation);
    bool Update(Reservation reservation);
    bool Delete(int id);
}