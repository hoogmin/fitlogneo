using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using FitLogNeo.Validation;

namespace FitLogNeo.Models;

public class FitnessLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string? UserId { get; set; } // Foreign key

    [ForeignKey(nameof(UserId))]
    public IdentityUser? User { get; set; }

    [Required]
    [PastOrTodayDate]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(100)]
    public string Exercise { get; set; }

    [Required]
    public int Reps { get; set; }

    [Required]
    public int Sets { get; set; }

    public double? Weight { get; set; }
}
