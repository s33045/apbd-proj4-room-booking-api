namespace RoomBookingApi.models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string BuildingCode { get; set; } = String.Empty;
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}