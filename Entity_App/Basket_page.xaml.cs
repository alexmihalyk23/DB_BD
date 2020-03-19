using Entity_App.Controllers;
using Entity_App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Entity_App
{
    public partial class Basket_page : Page
    {
        Controller controller = new Controller();

        public Basket_page()
        {
            InitializeComponent();
            Initialize_Basket();
        }

        private void Initialize_Basket()
        {
            int count_rows = 0, count_columns = 0;
            Category category = new Category();
            Basket basket = new Basket();
            List<Goods> list_goods = new List<Goods>();
            list_goods = controller.GetListGoodsId();


            foreach (var i in list_goods)
            {
                List<Basket> list = new List<Basket>();
                list = controller.GetBasketByLoginAndGoodsId(Convert.ToInt32(i.id));

                if (list.Count > 0) foreach (var j in list)
                    {
                        Button but = new Button();
                        Image image = new Image();

                        category = controller.GetCategoryById(Convert.ToInt32(i.id_category));

                        var stackPanel = new StackPanel { Background = new SolidColorBrush(Color.FromArgb(100, 242, 242, 242)), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Orientation = Orientation.Vertical, Width = 250, Height = 250 };
                        stackPanel.Children.Add(new Label { Margin = new System.Windows.Thickness(0, 10, 0, 0), VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Name = "Label_goods_name", Content = i.name });

                        if (i.image != null)
                        {
                            DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
                            Color myShadowColor = new Color();
                            myShadowColor.A = 100;
                            myShadowColor.R = 86;
                            myShadowColor.G = 84;
                            myShadowColor.B = 102;
                            myDropShadowEffect.Color = myShadowColor;
                            myDropShadowEffect.Direction = 320;
                            myDropShadowEffect.ShadowDepth = 25;
                            myDropShadowEffect.Softness = 1;
                            myDropShadowEffect.Opacity = 0.5;

                            stackPanel.BitmapEffect = myDropShadowEffect;


                            image.Source = new BitmapImage(new Uri(i.image));
                            image.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            image.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            image.Width = 70;
                            image.Height = 80;
                            stackPanel.Children.Add(image);
                        }

                        stackPanel.Children.Add(new Label { VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Name = "Label_goods_category", Content = "Категория: " + category.name });
                        stackPanel.Children.Add(new Label { VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Name = "Label_goods_price", Content = "Цена: " + Convert.ToString(i.price) });

                        but.Content = "Удалить";
                        but.Width = 100;
                        but.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        but.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        
                        int temp = Convert.ToInt32(j.id);
                        but.Click += (source, e) =>
                        {
                            controller.RemoveFromBasket(temp);
                            Grid_goods.Children.Remove((System.Windows.UIElement)stackPanel);
                        };

                        stackPanel.Children.Add(but);

                        if (count_columns >= 3)
                        {
                            count_columns = 0;
                            RowDefinition gridRow = new RowDefinition();
                            Grid_goods.RowDefinitions.Add(gridRow);
                            count_rows++;
                        }
                        Grid_goods.Children.Add(stackPanel);
                        Grid.SetColumn(stackPanel, count_columns);
                        Grid.SetRow(stackPanel, count_rows);
                        count_columns++;
                    }
                
            }
        }
    }
}