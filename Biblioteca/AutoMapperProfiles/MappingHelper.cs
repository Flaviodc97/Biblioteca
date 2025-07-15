using BibliotecaDAL.Entities;

namespace Biblioteca.AutoMapperProfiles
{
    public static class MappingHelper
    {
        public static int GetMaxLoansAllowed(MembershipType membershipType)
        {

            return membershipType switch
            {
                MembershipType.Free => 2,
                MembershipType.Standard => 5,
                MembershipType.Ultra => 10,
                _ => 0
            };
        }
    }
}
