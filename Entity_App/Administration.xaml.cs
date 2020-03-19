using Entity_App.Controllers;
using Entity_App.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Entity_App
{
    public partial class Administration : Window
    {
        Controller controller = new Controller();

        public ObservableCollection<MyData> MySource { get; set; }
        public ObservableCollection<MyData_c> MySource_c { get; set; }

        public struct MyData
        {
            public string name { set; get; }
            public decimal price { set; get; }
            public string category { set; get; }
            public string image_path { set; get; }
        }

        public struct MyData_c
        {
            public string name { set; get; }
        }

        public Administration()
        {
            InitializeComponent();
            MySource = new ObservableCollection<MyData>();
            MySource_c = new ObservableCollection<MyData_c>();
            ShowTable();
        }

        public void InitializeCombobox()
        {
            combobox_category.ItemsSource = null;
            combobox_category.ItemsSource = controller.GetListCategory();
        }

        public void ShowTable()
        {
            ClearAll();
            InitializeCombobox();
            UpdateTableGoods();
            UpdateTableCategories();
        }

        public void ClearAll()
        {
            textbox_name.Text = "";
            textbox_price.Text = "";
            textbox_image.Text = "";
            combobox_category.Text = "";
            textbox_name_c_now.Text = "";
            textbox_name_c_then.Text = "";
        }

        public void UpdateTableGoods()
        {
            datagrid_goods.Columns.Clear();
            datagrid_goods.ItemsSource = null;
            MySource.Clear();

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Width = 197.5;
            col1.Binding = new Binding("name");
            datagrid_goods.Columns.Add(col1);
            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Price";
            col2.Width = 189;
            col2.Binding = new Binding("price");
            datagrid_goods.Columns.Add(col2);
            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Category";
            col3.Width = 197.5;
            col3.Binding = new Binding("category");
            datagrid_goods.Columns.Add(col3);
            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Image";
            col4.Width = 197.5;
            col4.Binding = new Binding("image_path");
            datagrid_goods.Columns.Add(col4);

            string temp = "";
            foreach (var i in controller.GetListGoodsId())
            {
                if (i.id_category != null)
                {
                    temp = controller.GetCategoryById(Convert.ToInt32(i.id_category)).name;
                }

                MySource.Add(new MyData
                {
                    name = i.name,
                    price = (decimal)i.price,
                    category = temp,
                    image_path = i.image
                });


            }
            datagrid_goods.ItemsSource = MySource;
        }

        public void UpdateTableCategories()
        {
            datagrid_category.Columns.Clear();
            datagrid_category.ItemsSource = null;

            MySource_c.Clear();

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Width = 500;
            col1.Binding = new Binding("name");
            datagrid_category.Columns.Add(col1);

            List<string> list = new List<string>();
            list = controller.GetListCategory();

            foreach (var i in list)
            {
                MySource_c.Add(new MyData_c
                {
                    name = i
                });
            }

            datagrid_category.ItemsSource = MySource_c;
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_name.Text != "")
            {
                if (controller.GetGoodsByName(textbox_name.Text) != null)
                {
                    Goods goods = new Goods();
                    goods = controller.GetGoodsByName(textbox_name.Text);
                    int temp = 0;

                    if (textbox_price.Text != "" && int.TryParse(textbox_price.Text, out temp))
                    {
                        goods.price = Convert.ToDecimal(textbox_price.Text);
                        controller.dbUtils.SaveChanges();
                    }
                    else
                    {
                        string messageBoxText = "Do you want to clear the price of " + goods.name + "?";
                        string caption = "Warning";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                goods.price = 0;
                                controller.dbUtils.SaveChanges();
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                        }
                    }

                    if (combobox_category.Text != "")
                    {
                        goods.id_category = controller.GetCategoryByName(combobox_category.Text).id;
                        controller.dbUtils.SaveChanges();
                    }
                    else
                    {
                        string messageBoxText = "Do you want to set default value for category of " + goods.name + "?";
                        string caption = "Warning";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                goods.id_category = 1;
                                controller.dbUtils.SaveChanges();
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                        }
                    }

                    //Console.WriteLine(File.Exists(textbox_image.Text) ? "File exists." : "File does not exist.");

                    if (textbox_image.Text != "" && File.Exists(textbox_image.Text))
                    {
                        goods.image = textbox_image.Text;
                        controller.dbUtils.SaveChanges();
                    }
                    else
                    {
                        string messageBoxText = "Do you want to set default value for image path of " + goods.name + "?";
                        string caption = "Warning";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                goods.image = "C://Users//Vadim//source//repos//Entity_App//Entity_App//images//default.png";
                                controller.dbUtils.SaveChanges();
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                        }
                    }

                    ShowTable();
                }
                else
                {
                    MessageBox.Show("Product does'nt exists");
                }
            }
            else
            {
                MessageBox.Show("Enter data");
            }
        }

        private void Button_Insert_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_name.Text != "")
            {
                if (controller.GetGoodsByName(textbox_name.Text) != null)
                {
                    MessageBox.Show("Product exists");
                }
                else
                {
                    string name = textbox_name.Text;
                    string price_str = textbox_price.Text;
                    int Num, cat_id;
                    string im = textbox_image.Text;

                    bool isNum = int.TryParse(price_str, out Num);

                    if (isNum)
                    { Num = int.Parse(price_str); }
                    else { Num = 0; }

                    if (im != "" && File.Exists(im))
                    {}
                    else
                    {
                        im = "C://Users//Vadim//source//repos//Entity_App//Entity_App//images//default.png";
                    }

                    if (combobox_category.Text != "")
                    {
                        cat_id = controller.GetCategoryByName(combobox_category.Text).id;
                    }
                    else { cat_id = 1; }

                    controller.AddGoods(name, Num, cat_id, im);
                    ShowTable();
                }
            }
            else
            {
                MessageBox.Show("Enter data");
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_name.Text != "")
            {
                if (controller.GetGoodsByName(textbox_name.Text) != null)
                {
                    Goods goods = new Goods();
                    goods = controller.GetGoodsByName(textbox_name.Text);
                    controller.RemoveFromGoods(Convert.ToInt32(goods.id));
                    ShowTable();
                }
                else
                {
                    MessageBox.Show("Product does'nt exists");
                }
            }
            else
            {
                MessageBox.Show("Enter data");
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            new SignIn().Show();
            this.Close();
        }

        private void Button_Update_c_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_name_c_now.Text != "" && textbox_name_c_then.Text != "")
            {
                if (controller.GetCategoryByName(textbox_name_c_now.Text) != null && controller.GetCategoryByName(textbox_name_c_then.Text) == null)
                {
                    Category category = new Category();
                    category = controller.GetCategoryByName(textbox_name_c_now.Text);

                    string messageBoxText = "Do you want to change name of " + category.name + "?";
                    string caption = "Warning";
                    MessageBoxButton button = MessageBoxButton.OKCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                    switch (result)
                    {
                        case MessageBoxResult.OK:
                            category.name = textbox_name_c_then.Text;
                            controller.dbUtils.SaveChanges();
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }

                    ShowTable();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("Enter data");
            }
        }

        private void Button_Insert_c_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_name_c_now.Text != "")
            {
                if (controller.GetCategoryByName(textbox_name_c_now.Text) != null)
                {
                    MessageBox.Show("Product exists");
                }
                else
                {
                    string name = textbox_name_c_now.Text;

                    controller.AddCategory(name);
                    ShowTable();
                }
            }
            else
            {
                MessageBox.Show("Enter data");
            }
        }

        private void Button_Delete_c_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_name_c_now.Text != "")
            {
                if (controller.GetCategoryByName(textbox_name_c_now.Text) != null)
                {
                    Category category = new Category();
                    category = controller.GetCategoryByName(textbox_name_c_now.Text);
                    controller.RemoveFromCategory(Convert.ToInt32(category.id));
                    ShowTable();
                }
                else
                {
                    MessageBox.Show("Product does'nt exists");
                }
            }
            else
            {
                MessageBox.Show("Enter data");
            }
        }
    }
}