using System.Linq;
using Entity_App.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace Entity_App.Controllers
{
    class Controller
    {
        public Store_Entities dbUtils = Store_Singleton.init();
        public static string loginUser = "";

        public Controller() { }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            return dbUtils.User.Where(c =>
                c.login == login
                && c.password == password
                ).SingleOrDefault();
        }

        public User GetUserByLogin(string login)
        {
            return dbUtils.User.Where(c =>
                c.login == login
                ).SingleOrDefault();
        }

        public Role GetRoleByName(string name)
        {
            return dbUtils.Role.Where(c =>
                c.name == name
                ).SingleOrDefault();
        }

        public Goods GetGoodsByName(string name)
        {
            return dbUtils.Goods.Where(
                c =>
                c.name == name
                ).SingleOrDefault();
        }

        public Goods GetGoodsById(int id)
        {
            return dbUtils.Goods.Where(
                c =>
                c.id == id
                ).SingleOrDefault();
        }

        public List<Goods> GetListGoodsId()
        {
            List<Goods> goods = new List<Goods>();
            List<Goods> list = dbUtils.Goods.ToList();
            goods = list;
            return goods;
        }

        public List<string> GetListCategory()
        {
            List<string> category = new List<string>();
            List<Category> temp = new List<Category>();
            temp = dbUtils.Category.ToList();
            foreach (var i in temp)
            {
                category.Add(i.name.ToString());
            }

            return category;
        }

        public ObservableCollection<string> GetListRole()
        {
            ObservableCollection<string> roles = new ObservableCollection<string>();
            foreach(var i in dbUtils.Role.ToList())
            {
                roles.Add(i.name);
            }
            return roles;
        }

        public List<Basket> GetBasketByLoginAndGoodsId(int id)
        {
            List<Basket> baskets = new List<Basket>();
            User user = GetUserByLogin(
              login: loginUser
            );

            baskets = dbUtils.Basket.Where(
                    c =>
                    c.id_user == user.id
                    && c.id_goods == id
            ).ToList<Basket>();

            return baskets;
        }

        public void RemoveFromBasket(int id)
        {
            Basket basket = new Basket();
            basket = dbUtils.Basket.Where(
                c =>
                c.id == id
                ).SingleOrDefault();
            dbUtils.Basket.Remove(basket);
            dbUtils.SaveChanges();
        }

        public void RemoveFromGoods(int id)
        {
            Goods goods = new Goods();
            goods = dbUtils.Goods.Where(
                c =>
                c.id == id
                ).SingleOrDefault();
            dbUtils.Goods.Remove(goods);
            dbUtils.SaveChanges();
        }

        public void RemoveFromCategory(int id)
        {
            Category category = new Category();
            category = dbUtils.Category.Where(
                c =>
                c.id == id
                ).SingleOrDefault();
            dbUtils.Category.Remove(category);
            dbUtils.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return dbUtils.Category.Where(
                c =>
                c.id == id
                ).SingleOrDefault();
        }

        public Category GetCategoryByName(string name)
        {
            return dbUtils.Category.Where(
                c =>
                c.name == name
                ).SingleOrDefault();
        }

        public void AddUser(string login, string password, string name, string surname,
            string phone, string mail, int role_id)
        {
            dbUtils.User.Add(
                new User()
                {
                    name = name,
                    surname = surname,
                    phone = phone,
                    mail = mail,
                    login = login,
                    password = password,
                    id_role = role_id
                }
            );

            dbUtils.SaveChanges();
        }

        public void AddGoods(string name, decimal price, int category_id, string image_path)
        {
            dbUtils.Goods.Add(
                new Goods()
                {
                    name = name,
                    price = price,
                    id_category = category_id,
                    image = image_path
                }
            );

            dbUtils.SaveChanges();
        }

        public void AddCategory(string name)
        {
            dbUtils.Category.Add(
                new Category()
                {
                    name = name
                }
            );

            dbUtils.SaveChanges();
        }

        public void AddToBasket(string goodsName)
        {
            if (loginUser != "")
            {
                User user = GetUserByLogin(
                   login: loginUser
               );
                Goods goods = GetGoodsByName(
                    name: goodsName
               );

                dbUtils.Basket.Add(
                    new Basket()
                    {
                        id_user = user.id,
                        id_goods = goods.id,
                    }
                    );
                dbUtils.SaveChanges();
                
            }
            else { MessageBox.Show("Sign in to your account"); }
        }
    }
}