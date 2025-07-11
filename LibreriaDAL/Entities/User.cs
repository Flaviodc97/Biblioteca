using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public MembershipType MembershipType { get; set; }
        public int MaxLoansAllowed
        {
            get 
            {
                return MembershipType switch
                {
                    MembershipType.Free => 2,
                    MembershipType.Standard => 5,
                    MembershipType.Ultra => 10,
                    _ => 0
                };
            }
        }
        public IList<Notification> Notifications { get; set; }

        
    }
    public enum MembershipType
    { 
        Free,
        Standard,
        Ultra
    }
}
