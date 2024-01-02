using EventHandler.Data.Models;

namespace EventHandlerAPI.Views
{
    public class MemberView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Membership { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}