using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hash
{
    public partial class AdminForm : Form
    {
        // Cesta k XML souboru
        private static readonly string UsersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.xml");

        public AdminForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        // Načíst uživatele do ListBox
        private void LoadUsers()
        {
            if (File.Exists(UsersFilePath))
            {
                try
                {
                    XDocument doc = XDocument.Load(UsersFilePath);
                    var users = doc.Descendants("User")
                                   .Select(u => new
                                   {
                                       Username = u.Element("Username").Value,
                                       Role = u.Element("Role").Value
                                   })
                                   .ToList();

                    lstUsers.Items.Clear(); // Vymazat předchozí položky
                    foreach (var user in users)
                    {
                        lstUsers.Items.Add($"{user.Username} ({user.Role})");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při načítání souboru: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Soubor s uživateli nebyl nalezen.");
            }
        }

        

        // Obsluhování výběru v ListBox (Pokud chcete zobrazit podrobnosti o uživateli)
        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string selectedUser = lstUsers.SelectedItem.ToString();
                string username = selectedUser.Split('(')[0].Trim();

                // Zde můžete zobrazit podrobnosti o uživateli, pokud byste chtěli
                // Například můžete nastavit textové pole nebo jiný ovládací prvek
            }
        }

        // Změna hesla pro vybraného uživatele
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Ověření, jestli je nějaký uživatel vybrán v ListBox
            if (lstUsers.SelectedItem != null && !string.IsNullOrEmpty(txtNewPassword.Text))
            {
                string selectedUser = lstUsers.SelectedItem.ToString();
                string username = selectedUser.Split('(')[0].Trim(); // Extrahujeme uživatelské jméno (před závorkou)

                try
                {
                    XDocument doc = XDocument.Load(UsersFilePath);

                    // Najdeme uživatele podle jména
                    var userElement = doc.Descendants("User")
                                         .FirstOrDefault(u => u.Element("Username").Value == username);

                    if (userElement != null)
                    {
                        // Nastavíme nové heslo
                        string newPassword = txtNewPassword.Text;
                        userElement.Element("Password").Value = newPassword;
                        userElement.Element("HashedPassword").Value = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(newPassword)); // Předpokládáme, že HashedPassword je Base64 enkódovaná

                        // Uložíme změny zpět do XML souboru
                        doc.Save(UsersFilePath);
                        MessageBox.Show($"Heslo pro uživatele {username} bylo změněno.");
                        LoadUsers(); // Obnovíme seznam uživatelů v ListBox
                    }
                    else
                    {
                        MessageBox.Show("Uživatel nebyl nalezen.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při změně hesla: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Prosím vyberte uživatele a zadejte nové heslo.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["LoginForm"].Show();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUser.Text.Trim();

            if (string.IsNullOrEmpty(newUsername))
            {
                MessageBox.Show("Zadejte uživatelské jméno.");
                return;
            }

            try
            {
                XDocument doc = XDocument.Load(UsersFilePath);

                // Ověření, zda už uživatel existuje
                var existingUser = doc.Descendants("User")
                                      .FirstOrDefault(u => u.Element("Username").Value == newUsername);

                if (existingUser != null)
                {
                    MessageBox.Show("Uživatel již existuje.");
                    return;
                }

                // Vytvoření nového uživatele
                XElement newUser = new XElement("User",
                    new XElement("Username", newUsername),
                    new XElement("Password", ""),  // Prázdné heslo
                    new XElement("HashedPassword", ""),  // Prázdné heslo
                    new XElement("Role", "User") // Standardní role
                );

                doc.Element("Users").Add(newUser);
                doc.Save(UsersFilePath);

                MessageBox.Show($"Uživatel {newUsername} byl založen. Nyní je možné nastavit heslo.");
                txtNewUser.Clear();
                LoadUsers(); // Obnoví listbox

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při přidávání uživatele: {ex.Message}");
            }
        }


    }
}
