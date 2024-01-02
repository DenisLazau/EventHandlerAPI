using EventHandler.Data.Models;

namespace EventHandlerAPI.Views
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