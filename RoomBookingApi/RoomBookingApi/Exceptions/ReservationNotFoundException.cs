namespace RoomBookingApi.Exceptions;

public class ReservationNotFoundException(int id) : Exception($"Reservation with {id} not found");