using Entity_App.Controllers;
using Entity_App.Entity;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Entity_App
{
    public partial class Registration : Window
    {
        Controller controller = new Controller();
        public Registration()
        {
            InitializeComponent();
            combobox_role.ItemsSource = controller.GetListRole();
        }

        private void Button_sign_up_Click(object sender, RoutedEventArgs e)
        {
            Role role = new Role();
            string messageBoxText = "";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            if (combobox_role.Text != "" && textbox_login.Text != "" && passwordbox_1.Password != "" 
                && passwordbox_2.Password != "" && (passwordbox_1.Password == passwordbox_2.Password))
            {
                messageBoxText = "Do you want to sign up?";
                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
                role = controller.GetRoleByName(combobox_role.Text);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        controller.AddUser(textbox_login.Text, passwordbox_1.Password, textbox_name.Text, textbox_surname.Text, textbox_phone.Text,
                            textbox_mail.Text, role.id);
                        new SignIn().Show();
                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Incorrect password or blank required fields");
            }
        }
    }
}
