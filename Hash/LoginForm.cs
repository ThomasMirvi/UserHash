﻿using System;
using System.IO;
using System.Linq;
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
                    var users = doc.Descendants("User")
                                   .Where(u => u.Element("Username").Value == txtUsername.Text
                                               && u.Element("Password").Value == txtPassword.Text)
                                   .ToList();

                    if (users.Any())
                    {
                        var user = users.First();

                        if (user.Element("Role").Value == "Admin")
                        {
                            AdminForm adminForm = new AdminForm();
                            adminForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Uživatel nemá administrátorskou roli.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Neplatné přihlašovací údaje.");
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
