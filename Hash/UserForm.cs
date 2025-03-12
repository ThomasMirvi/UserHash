using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hash
{
    public partial class UserForm : Form
    {
        private string username;
        private static readonly string UsersFilePath = Path.Combine(Application.StartupPath, "users.xml");

        public UserForm(string username)
        {
            InitializeComponent();
            this.username = username;
            lblUser.Text = username;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Zavře UserForm
            Application.OpenForms["LoginForm"].Show(); // Znovu zobrazí LoginForm
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Zadejte nové heslo.");
                return;
            }

            try
            {
                if (File.Exists(UsersFilePath))
                {
                    XDocument doc = XDocument.Load(UsersFilePath);

                    var userElement = doc.Descendants("User")
                                         .FirstOrDefault(u => u.Element("Username").Value == username);

                    if (userElement != null)
                    {
                        string newPassword = txtNewPassword.Text;

                        // Aktualizujeme pouze HashedPassword
                        userElement.Element("HashedPassword").Value = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(newPassword));

                        doc.Save(UsersFilePath);
                        MessageBox.Show("Heslo bylo úspěšně změněno.");
                        txtNewPassword.Clear(); // Vymazat textbox
                    }
                    else
                    {
                        MessageBox.Show("Uživatel nebyl nalezen.");
                    }
                }
                else
                {
                    MessageBox.Show("Soubor s uživateli nebyl nalezen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při změně hesla: {ex.Message}");
            }
        }
    }
}
