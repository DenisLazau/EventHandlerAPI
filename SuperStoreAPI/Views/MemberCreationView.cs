using SuperStore.Data.Models;

namespace SuperStoreAPI.Views
{
    public class MemberCreationView
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Membership { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}