using Entity_App.Controllers;
using System;
using System.Windows;

namespace Entity_App
{
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            Frame_Client.NavigationService.Navigate(new Uri("Product_of_the_day.xaml", UriKind.Relative));
        }

        private void But_prod_of_day_Click(object sender, RoutedEventArgs e)
        {
            Frame_Client.NavigationService.Navigate(new Uri("Product_of_the_day.xaml", UriKind.Relative));
        }

        private void But_prod_Click(object sender, RoutedEventArgs e)
        {
            Frame_Client.NavigationService.Navigate(new Uri("Goods_page.xaml", UriKind.Relative));
        }

        private void But_basket_Click(object sender, RoutedEventArgs e)
        {
            Frame_Client.NavigationService.Navigate(new Uri("Basket_page.xaml", UriKind.Relative));
        }

        private void But_account_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            if (Controller.loginUser != "")
            {
                messageBoxText = "Do you want to log out?";
                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result)
                {
                    case MessageBoxResult.OK:
                        Controller.loginUser = "";
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                messageBoxText = "Do you want to sing in?";
                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result)
                {
                    case MessageBoxResult.OK:
                        new SignIn().Show();
                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }

        private void But_info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This page is under development");
        }

        private void But_notification_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This page is under development");
        }

        private void But_settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This page is under development");
        }

        private void But_off_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
