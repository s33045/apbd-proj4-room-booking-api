namespace RoomBookingApi.Exceptions;

public class RoomNotFoundException(int id) : Exception($"Room with {id} not found");