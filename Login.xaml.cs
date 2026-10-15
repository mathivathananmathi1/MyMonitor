using System;
using System.IO;
using System.Windows;

namespace MyMonitor
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public string ErrorDisply(string input, string message)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return message;
            }
            return "";
        }

        public bool isValue(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        // REGISTER BUTTON CLICK
        private void btnreg_Click(object sender, RoutedEventArgs e)
        {
            if (isValue(txtreguser.Text) == true)
            {
                txtreguser.Text = txtreguser.Text.ToUpper();
            }

            // 1. Mandatory Fields Validation
            erruser.Text = ErrorDisply(txtreguser.Text, "Please enter user name");
            errpassword.Text = ErrorDisply(pwregpassword.Password, "Please enter password");
            errage.Text = ErrorDisply(txtage.Text, "Please enter age");
            erremail.Text = ErrorDisply(txtemail.Text, "Please enter Email");

            bool isValid = true;

            // 2. Age Validation Logic
            if (isValue(txtage.Text) == true)
            {
                try
                {
                    int age = Convert.ToInt16(txtage.Text);
                    if (age > 18 == false)
                    {
                        errage.Text = "Age should more than 18";
                        isValid = false;
                    }
                    else
                    {
                        errage.Text = "";
                    }
                }
                catch
                {
                    errage.Text = "Please enter a valid number";
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            // 3. Email Validation Logic
            if (isValue(txtemail.Text) == true)
            {
                if (txtemail.Text.Contains("@") == true && txtemail.Text.Contains("."))
                {
                    erremail.Text = "";
                }
                else
                {
                    erremail.Text = "Please enter valid email";
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtreguser.Text) || string.IsNullOrWhiteSpace(pwregpassword.Password))
            {
                isValid = false;
            }

            // 4. File Creation Logic
            if (isValid)
            {
                string mainAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                mainAppData = Path.Combine(mainAppData, "MyMonitor");

                if (!Directory.Exists(mainAppData))
                {
                    Directory.CreateDirectory(mainAppData);
                }

                if (!string.IsNullOrWhiteSpace(txtreguser.Text))
                {
                    string filename = Path.Combine(mainAppData, txtreguser.Text + ".txt");

                    if (File.Exists(filename))
                    {
                        MessageBox.Show("User Name already exist");
                    }
                    else
                    {
                        string content = txtreguser.Text + "|" + pwregpassword.Password + "|" + txtage.Text + "|" + txtemail.Text;
                        File.WriteAllText(filename, content);
                        MessageBox.Show("Registration Successful!");
                    }
                }
            }
        }

        // LOGIN BUTTON CLICK
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtloginuser.Text))
            {
                string mainAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                mainAppData = Path.Combine(mainAppData, "MyMonitor");

                string filename = Path.Combine(mainAppData, txtloginuser.Text.ToUpper() + ".txt");

                if (File.Exists(filename))
                {
                    string content = File.ReadAllText(filename);
                    string[] splitcontent = content.Split('|');

                    if (splitcontent[0] == txtloginuser.Text.ToUpper() && splitcontent[1] == pwloginpassword.Password)
                    {
                        MessageBox.Show("Login Success");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Password");
                    }
                }
                else
                {
                    MessageBox.Show("User does not exist");
                }
            }
            else
            {
                MessageBox.Show("Please enter user name");
            }
        }
    }
}