using System.ComponentModel.DataAnnotations;

namespace RoomBookingApi.DTOs;

public class UpdateRoomDto
{
    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(20)] public string BuildingCode { get; set; } = string.Empty;
    public int Floor { get; set; }
    [Range(1, int.MaxValue)] public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}