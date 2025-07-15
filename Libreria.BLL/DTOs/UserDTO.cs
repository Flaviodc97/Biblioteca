using BibliotecaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.DTOs
{
    public record UserDTO(
    int Id,
    string Name,
    string Surname,
    DateTime DateOfBirth,
    string PhoneNumber,
    string PostalCode,
    string Address,
    string City,
    DateTime MembershipStartDate,
    DateTime MembershipEndDate,
    string MembershipType,
    int MaxLoansAllowed
        );

    

}

