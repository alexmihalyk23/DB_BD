using System.Windows;
using Entity_App.Entity;
using Entity_App.Controllers;

namespace Entity_App
{
    public partial class SignIn : Window
    {
        Controller controller = new Controller();

        public SignIn()
        {
            InitializeComponent();
        }

        private void button_signin_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_signin.Text.Equals("") || passwordbox_signin.Password.Equals(""))
            {
                MessageBox.Show("Enter data");
                return;
            }
            User user = controller.GetUserByLoginAndPassword(
                login: textbox_signin.Text,
                password: passwordbox_signin.Password
            );

            if (user == null)
            {
                MessageBox.Show("Account isn't found");
                return;
            }
            else { Controller.loginUser = user.login; }

            if (controller.GetUserByLoginAndPassword(textbox_signin.Text, passwordbox_signin.Password).id_role == controller.GetRoleByName("Client").id)
            {
                new ClientWindow().Show();
                this.Close();
            }
            else if (controller.GetUserByLoginAndPassword(textbox_signin.Text, passwordbox_signin.Password).id_role == controller.GetRoleByName("Administrator").id)
            {
                new Administration().Show();
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_reg_Click(object sender, RoutedEventArgs e)
        {
            new Registration().Show();
            this.Close();
        }
    }
}

