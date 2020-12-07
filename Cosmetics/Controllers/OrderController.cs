using API_NganLuong;
using NongSan.Common;
using NongSan.Models;
using NongSan.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSan.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [Route("gio-hang")]
        public ActionResult Index()
        {
            var cart = ShoppingCart.Cart;
            return View(cart.Items);
        }
        public JsonResult Add(int id, int qtyPro)
        {
            var cart = ShoppingCart.Cart;
            cart.Add(id, qtyPro);
            var info = new { cart.Count, cart.CartTotal, qtyPro };
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(int id, int quatity)
        {
            var cart = ShoppingCart.Cart;
            cart.Update(id, quatity);
            var p = cart.Items.Single(x => x.Product.Id == id);
            var info = new
            {
                cart.Count,
                cart.CartTotal,
                Amount = (p.Product.Price * p.Quantity),
                TotalItem = (p.Product.Price * p.Quantity)
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payment()
        {
            var userLogin = Utility.GetLoginUser();
            if (string.IsNullOrEmpty(userLogin))
            {
                return RedirectToAction("Login", "Customer");
            }
            NongSanEntities db = new NongSanEntities();
            var cart = ShoppingCart.Cart;

            var user = db.Customers.FirstOrDefault(x => x.UserName == userLogin);
            if (user == null)
            {
                user = new Customer();
            }
            ViewData["Customer"] = user;
            return View(cart.Items);
        }



        public JsonResult CheckOut(string type, string bank)
        {
            var userLogin = Utility.GetLoginUser();

            NongSanEntities db = new NongSanEntities();
            var cus = db.Customers.FirstOrDefault(x => x.UserName == userLogin);
            var cart = ShoppingCart.Cart;

            bool result = false;
            var model = new Order();
            OrderDAO dao = new OrderDAO();
            model.DateCreate = DateTime.Now;
            model.IsDelete = false;
            model.SearchText = dao.GetSearchText(model);
            model.OD_ID = dao.GetNextID();
            model.TotalAmount = cart.CartTotal;
            model.CreateBy = userLogin;
            model.StatusID = 1;

            model.PhoneNumber = cus.Phone;
            model.FullName = cus.Name;
            model.Email = cus.Email;

            db.Orders.Add(model);
            result = db.SaveChanges() > 0;

            foreach (var item in cart.Items)
            {
                var od = new OrderDetail
                {
                    OrderID = model.ID,
                    Price = Convert.ToDouble(item.Product.Price),
                    ProductName = item.Product.ProductName,
                    Quantity = item.Quantity,
                    ProductID = item.Product.Id,
                    IsDelete = false
                };
                db.OrderDetails.Add(od);
                result = db.SaveChanges() > 0;
            }
            HttpCookie StudentCookies = new HttpCookie("OrderSuccess", model.ID.ToString());
            StudentCookies.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(StudentCookies);
            Session.Remove("Cart");
            //if (type != "cod")
            //{
            //    string url = PaymentBank(model, bank);
            //}
            SendMail(model, cus, cart.Items);
            if (result)
                return Json(new { Code = 1 });
            return Json(new { Code = -1 });

        }

        public JsonResult Remove(int id)
        {
            var cart = ShoppingCart.Cart;
            cart.Remove(id);
            var info = new { cart.CartTotal, cart.Count };
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [Route("dat-hang/{id}")]
        public ActionResult AddNew(int id)
        {
            NongSanEntities db = new NongSanEntities();
            var pro = db.Products.FirstOrDefault(x => x.Id == id);
            if (pro != null)
            {
                ViewData["Product"] = pro;
            }

            return View();
        }

        public ActionResult Success()
        {
            NongSanEntities db = new NongSanEntities();
            var id = Convert.ToInt32(Request.Cookies["OrderSuccess"].Value);
            var model = db.Orders.FirstOrDefault(x => x.ID == id);
            return View(model);
        }
        public string PaymentBank(Order od, string bankcode)
        {
            string reponse = "";
            RequestInfo info = new RequestInfo();
            info.Merchant_id = "64301";
            info.Merchant_password = "0fd32937195bc2bab6d35d9e462cc5e7";
            info.Receiver_email = "dthoai318@gmail.com";



            info.cur_code = "vnd";
            info.bank_code = bankcode;

            info.Order_code = od.OD_ID;
            info.Total_amount = od.TotalAmount.ToString();
            info.fee_shipping = "0";
            info.Discount_amount = "0";
            info.order_description = "Thanh toan cho dong hang " + od.OD_ID;
            info.return_url = "http://localhost:28752/";
            info.cancel_url = "http://localhost:28752/";

            info.Buyer_fullname = od.FullName;
            info.Buyer_email = od.Email;
            info.Buyer_mobile = od.PhoneNumber;

            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseInfo result = objNLChecout.GetUrlCheckout(info, "ATM_ONLINE");

            if (result.Error_code == "00")
            {
                reponse = result.Checkout_url;
            }
            else
                reponse = "NG";
            return reponse;
        }
        public void SendMail(Order od, Customer cus, List<CartItem> items)
        {
            string productID = "";
            foreach (var item in items)
            {
                productID += item.Quantity + "x" + item.Product.ProductName + "<br/>";
            }

            string smtpUserName = "phuyen318@gmail.com";
            string smtpPassword = "twbfxtnzoxleagdp";
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 25;
            string bd = "";
            bd += string.Format("<div class='block'>" +

                        "<table border = '0'+ width='700' cellspacing ='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                           "<tr>" +
                                "<td align ='center'><img  src='http://demo1.cloodo.com/html/freshmart/img/logo.png'  width='300' height='200' border='0' /></a></td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td width ='100%' height='20'>&nbsp;</td>" +
                            "</tr>" +

                            "<tr>" +
                                "<td style ='font-family: Helvetica, arial, sans-serif; font-size: 18px; color: #333333; text-align: left; line-height: 20px;text-align:center'>THÔNG TIN ĐƠN HÀNG</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td width = '100%' height='20'>&nbsp;</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td width ='100%' height='20'><b>Họ Tên: </b>{0}</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td width = '100%' height='20'><b>Email: </b>{1}</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td width ='100%' height='20'><b>SDT: </b>{2}</td>" +
                            "</tr>" +
                             "<tr>" +
                                "<td width ='100%' height='20'><b>Sản phẩm: </b>{3}</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td width = '100%' height='20'><b>Tổng Tiền: </b> {4}</td>" +
                            "</tr>" +
                            "<tr></tr>" +
                        "</tbody>" +
                        "</table>" + "</div>", cus.Name, cus.Email, cus.Phone, productID, od.TotalAmount);


            string emailTo = cus.Email;
            string subject = "FreshMart xác nhận đặt hàng thành công";

            string body = bd;

            EmailService service = new EmailService();

            bool result = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                  emailTo, subject, body);
            var a = result;
        }
    }
}