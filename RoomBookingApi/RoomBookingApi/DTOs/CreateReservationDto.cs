using System.ComponentModel.DataAnnotations;
using RoomBookingApi.Models.Enums;

namespace RoomBookingApi.DTOs;

public class CreateReservationDto
{
    public int RoomId { get; set; }

    [Required] [MaxLength(100)] public string OrganizerName { get; set; } = string.Empty;

    [Required] [MaxLength(200)] public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public ReservationStatus Status { get; set; }
}