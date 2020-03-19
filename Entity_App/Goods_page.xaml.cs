using Entity_App.Controllers;
using Entity_App.Entity;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Entity_App
{
    public partial class Goods_page : Page
    {
        Controller controller = new Controller();

        public Goods_page()
        {
            InitializeComponent();
            Initialize_Goods();
        }

        private void Initialize_Goods()
        {
            int count_rows = 0, count_columns = 0;
            List<Goods> goods = new List<Goods>();
            Category category = new Category();
            goods = controller.GetListGoodsId();

            foreach (var obj in goods)
            {
                category = controller.GetCategoryById(Convert.ToInt32(obj.id_category));

                Button but = new Button();
                //but.Background = new SolidColorBrush(Color.FromArgb(100, 0, 113, 227));


                var stackPanel = new StackPanel { Background = new SolidColorBrush(Color.FromArgb(100, 242,242,242)), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Orientation = Orientation.Vertical, Width = 250, Height = 250 };
                stackPanel.Children.Add(new Label { Margin = new System.Windows.Thickness(10, 10, 10, 10), VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Name = "Label_goods_name", Content = obj.name });

                if (obj.image != null)
                {
                    Image image = new Image();
                    DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
                    Color myShadowColor = new Color();
                    myShadowColor.A = 100;
                    myShadowColor.R = 143;
                    myShadowColor.G = 143;
                    myShadowColor.B = 143;
                    myDropShadowEffect.Color = myShadowColor;
                    myDropShadowEffect.Direction = 320;
                    myDropShadowEffect.ShadowDepth = 25;
                    myDropShadowEffect.Softness = 1;
                    myDropShadowEffect.Opacity = 0.5;

                    stackPanel.BitmapEffect = myDropShadowEffect;
                    Console.WriteLine(obj.image);
                    image.Source = new BitmapImage(new Uri(obj.image));//Может нахуй это все
                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    image.Width = 70;
                    image.Height = 80;
                    stackPanel.Children.Add(image);
                }




                stackPanel.Children.Add(new Label { VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Name = "Label_goods_category", Content = "Категория: " + category.name });
                stackPanel.Children.Add(new Label { VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Name = "Label_goods_price", Content = "Цена: " + Convert.ToString(obj.price) });

                but.Content = "В корзину";
                but.Width = 100;
                but.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                but.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                
                string temp = obj.name;
                but.Click += (source, e) =>
                {
                    controller.AddToBasket(temp);
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
