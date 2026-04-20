using RoomBookingApi.Models;
using RoomBookingApi.Models.Enums;

namespace RoomBookingApi.Repositories;

public class ReservationRepository : IReservationRepository
{
    private static int _nextId = 1;
    private static readonly List<Reservation> _reservations = [];

    public IEnumerable<Reservation> GetAll()
    {
        return _reservations;
    }

    public Reservation? GetById(int id)
    {
        return _reservations.FirstOrDefault(reservation => reservation.Id == id);
    }

    public IEnumerable<Reservation> GetFiltered(DateOnly? date, ReservationStatus? status, int? roomId)
    {
        var query = _reservations.AsEnumerable();

        if (date.HasValue)
            query = query.Where(reservation => reservation.Date == date.Value);

        if (status.HasValue)
            query = query.Where(reservation => reservation.Status == status.Value);

        if (roomId.HasValue)
            query = query.Where(reservation => reservation.RoomId == roomId.Value);

        return query;
    }

    public void Add(Reservation reservation)
    {
        reservation.Id = _nextId++;
        _reservations.Add(reservation);
    }

    public bool Update(Reservation reservation)
    {
        var existing = GetById(reservation.Id);
        if (existing == null) return false;

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return true;
    }

    public bool Delete(int id)
    {
        var reservation = GetById(id);
        if (reservation == null) return false;
        _reservations.Remove(reservation);
        return true;
    }

    public bool Exists(int id)
    {
        return _reservations.Any(reservation => reservation.Id == id);
    }
}