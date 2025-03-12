using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hash
{
    public partial class LoginForm : Form
    {
        private static readonly string UsersFilePath = Path.Combine(Application.StartupPath, "users.xml");

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (File.Exists(UsersFilePath))
            {
                try
                {
                    XDocument doc = XDocument.Load(UsersFilePath);

                    // Převod zadaného hesla na Base64
                    string enteredPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(txtPassword.Text));

                    var user = doc.Descendants("User")
                                  .FirstOrDefault(u => u.Element("Username").Value == txtUsername.Text);

                    if (user != null)
                    {
                        string storedHashedPassword = user.Element("HashedPassword").Value;

                        if (storedHashedPassword == enteredPassword)
                        {
                            string role = user.Element("Role").Value;
                            string username = user.Element("Username").Value;

                            if (role == "Admin")
                            {
                                AdminForm adminForm = new AdminForm();
                                adminForm.Show();
                            }
                            else
                            {
                                UserForm userForm = new UserForm(username);
                                userForm.Show();
                            }

                            this.Hide(); // Skryje přihlašovací formulář
                        }
                        else
                        {
                            MessageBox.Show("Neplatné přihlašovací údaje.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uživatel neexistuje.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při načítání souboru: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show($"Soubor {UsersFilePath} nebyl nalezen.");
            }
        }
    }
}
