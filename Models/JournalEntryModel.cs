using System.ComponentModel.DataAnnotations;

namespace AdhdJournalApi.Models;

public class JournalEntryModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Summary { get; set; }

    [Required]
    [Range(1, 10)]
    public int MoodScale { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string Description { get; set; }

    [Required]
    public bool IsPhoneUsed { get; set; }

    public string ReflectionNote { get; set; }

    [Required]
    public bool IsSatisfied { get; set; }   
}


