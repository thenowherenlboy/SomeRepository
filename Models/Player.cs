
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
public class Player
{
    [KeyAttribute]
    public long Id { get; set; }
    [Required]
    public long TeamId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}