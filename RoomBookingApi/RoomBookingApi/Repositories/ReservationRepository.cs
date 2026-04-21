using RoomBookingApi.Models;
using RoomBookingApi.Models.Enums;

namespace RoomBookingApi.Repositories;

public class ReservationRepository : IReservationRepository
{
    private static int _nextId = 6; // hardcoded just for tests

    private static readonly List<Reservation> _reservations =
    [
        new()
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Jan Kowalski",
            Topic = "Planowanie biegów",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 0),
            Status = ReservationStatus.Confirmed
        },
        new()
        {
            Id = 2,
            RoomId = 1,
            OrganizerName = "Anna Nowak",
            Topic = "Nauka",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 30),
            EndTime = new TimeOnly(11, 30),
            Status = ReservationStatus.Planned
        },
        new()
        {
            Id = 3,
            RoomId = 2,
            OrganizerName = "Piotr Wisniewski",
            Topic = "Spotkanie z klientem",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(13, 0),
            Status = ReservationStatus.Confirmed
        },
        new()
        {
            Id = 4,
            RoomId = 3,
            OrganizerName = "Katarzyna Zielinska",
            Topic = "Warsztaty",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(9, 15),
            Status = ReservationStatus.Cancelled
        },
        new()
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "Marek Lewandowski",
            Topic = "Vlog",
            Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(14, 30),
            Status = ReservationStatus.Confirmed
        }
    ];

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

    public bool AnyForRoom(int roomId)
    {
        return _reservations.Any(reservation => reservation.RoomId == roomId);
    }
}