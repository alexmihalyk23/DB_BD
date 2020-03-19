using Entity_App.Controllers;
using Entity_App.Entity;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Entity_App
{
    public partial class Product_of_the_day : Page
    {
        Controller controller = new Controller();

        public Product_of_the_day()
        {
            InitializeComponent();
            
        }

        private void Initialize_Product(string prod_name)
        {
            Goods goods = controller.GetGoodsByName( name: prod_name );
            Category category = controller.GetCategoryById(id: Convert.ToInt32(goods.id_category));
           
            string temp = goods.name;
            controller.AddToBasket(temp);
            
        }

        private void AddToBasket_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Initialize_Product("Mac Pro");
        }
    }
}
