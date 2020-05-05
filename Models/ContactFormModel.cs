﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Karate_GSP.Models
{
    public class ContactFormModel
{
    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Message { get; set; }
}
}
