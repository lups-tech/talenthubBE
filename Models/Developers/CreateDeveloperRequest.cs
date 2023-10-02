using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


public class CreateDeveloperRequest
{
    [Required]
    public String? Name {get; set;}
    [Required]
    public String? Email {get; set;}
}
