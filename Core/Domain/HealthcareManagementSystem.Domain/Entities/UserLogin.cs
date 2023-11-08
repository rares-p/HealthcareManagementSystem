namespace HealthcareManagementSystem.Domain.Entities
{
    public class UserLogin
    {
        private UserLogin(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set;}
        public string Password { get; private set;}
        public string Email { get; private set;}
    }
}
