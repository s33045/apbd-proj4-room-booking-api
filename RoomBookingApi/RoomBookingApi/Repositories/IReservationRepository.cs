using RoomBookingApi.Models;
using RoomBookingApi.Models.Enums;

namespace RoomBookingApi.Repositories;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAll();
    Reservation? GetById(int id);
    IEnumerable<Reservation> GetFiltered(DateOnly? date, ReservationStatus? status, int? roomId);
    void Add(Reservation reservation);
    bool Update(Reservation reservation);
    bool Delete(int id);
    bool Exists(int id);
    bool AnyForRoom(int roomId);
}