using NongSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace NongSan.Models
{
    public class ShoppingCart
    {
        NongSanEntities db = new NongSanEntities();
        public List<CartItem> Items = new List<CartItem>();
        public static ShoppingCart Cart
        {
            get
            {
                var cart = HttpContext.Current.Session["Cart"] as ShoppingCart;
                if (cart == null)
                {
                    cart = new ShoppingCart();
                    HttpContext.Current.Session["Cart"] = cart;
                

                }
                return cart;
            }
        }
        public void Add(int id, int qty)
        {
            try
            {
                var item = Items.Single(x => x.Product.Id == id);
                item.Quantity += qty;
                item.TotalItem = item.Quantity * item.Product.Price;
            }
            catch
            {
                CartItem item = new CartItem();
                var pro = db.Products.FirstOrDefault(x => x.Id == id);
                item.Product = pro;
                item.Quantity = qty;
                item.TotalItem = item.Quantity * item.Product.Price;
                Items.Add(item);
            }
        }
        public void Update(int id, int newQuantity)
        {
            var item = Items.Single(x => x.Product.Id == id);
            item.Quantity = newQuantity;
        }
        public void Remove(int id)
        {
            var item = Items.Single(x => x.Product.Id == id);
            Items.Remove(item);
        }
        public int Count
        {
            get
            {
                return Items.Sum(x => x.Quantity);
            }
        }
        public decimal CartTotal
        {
            get
            {
                return Items.Sum(x => x.Product.Price * x.Quantity);
            }
        }
         
    }
}