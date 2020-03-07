using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
public class Team
{
    [KeyAttribute]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
}