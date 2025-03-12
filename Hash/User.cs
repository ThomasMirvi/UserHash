namespace Hash
{
    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }

        public User(string username, string password, string hashedPassword, string role)
        {
            Username = username;
            Password = password;
            HashedPassword = hashedPassword;
            Role = role;
        }
    }
}
