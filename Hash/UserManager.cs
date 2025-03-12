using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Hash
{
    internal class UserManager
    {
        private static readonly string UsersFilePath = "users.xml"; // Cesta k XML souboru

        public static List<User> LoadUsers()
        {
            var users = new List<User>();

            // Načtení XML souboru
            XDocument doc = XDocument.Load(UsersFilePath);

            var userElements = doc.Descendants("User");
            foreach (var userElement in userElements)
            {
                string username = userElement.Element("Username")?.Value;
                string password = userElement.Element("Password")?.Value;
                string hashedPassword = userElement.Element("HashedPassword")?.Value;
                string role = userElement.Element("Role")?.Value;

                if (username != null && password != null && hashedPassword != null && role != null)
                {
                    users.Add(new User(username, password, hashedPassword, role));
                }
            }

            return users;
        }

        public static bool ValidateLogin(string username, string password, out User authenticatedUser)
        {
            var users = LoadUsers();
            authenticatedUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return authenticatedUser != null;
        }
    }
}
