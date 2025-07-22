using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.DTOs.AuthorDTOS
{
    public record AuthorSeachDTO
    ( 
        string? Name,
        string? LastName,
        DateTime? StartDateOfBirth,
        DateTime? EndDateOfBirth,
        DateTime? StartDateOfDeath,
        DateTime? EndDateOfDeath,
        string? Nationality
        );
}
