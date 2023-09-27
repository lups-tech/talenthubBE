
namespace talenthubBE.Models.Users
{
    public class UserDeveloperRequest
    {
        public required String UserId {get; set;}
        public Guid DeveloperId {get; set;}
    }
}