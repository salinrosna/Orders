using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cobra_Onboarding.Models;

namespace Cobra_Onboarding.Controllers
{
    public class CobraOrderController : Controller
    {
        // GET: CobraOrder
        public ActionResult Order()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Getorders()
        {
            using (var db = new CobraOnboarddbEntities())   
            {
                
                var orders = db.OrderHeaders.Join(db.People, ord => ord.PersonId, peo => peo.Id,
                    (ord, peo) => new { ord, peo })
                    .Join(db.OrderDetails, od=>od.ord.Id, odt =>odt.OderId, (od,odt)=> new { od, odt})
                    .Join(db.Products, p=>p.odt.ProductId, pro => pro.Id ,(p,pro) =>new {p,pro })
                    .Select(x => new { x.p.od.ord.Id ,x.p.od.ord.OrderDate, x.p.od.peo, x.pro, x.pro.Price}).ToList();
                return new JsonResult { Data=orders,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
            }
            
        }
        [HttpGet]
        public JsonResult GetCP()
        {
            using (var db = new CobraOnboarddbEntities())
            {
                var list = new GetCPViewmodel();
                {
                    list.Person = db.People.Where(x=>x.IsActive==true).ToList();
                    list.Product = db.Products.ToList();
                }

            return new JsonResult { Data = list,JsonRequestBehavior=JsonRequestBehavior.AllowGet};
        }
        }
        [HttpPost]
        public JsonResult AddOrder(OrderViewModel order)
        {
            using (var db = new CobraOnboarddbEntities()) 
            {
                var orderheader = new OrderHeader()
                {
                    OrderDate = DateTime.Now,
                    PersonId = order.CustomerId
                };
                db.OrderHeaders.Add(orderheader);
                db.SaveChanges();
                var orderdetails = new OrderDetail()
                {
                    OderId=orderheader.Id,
                   ProductId = order.ProductId 
                };
                db.OrderDetails.Add(orderdetails);
                db.SaveChanges();
            }

            return new JsonResult();
        }

        [HttpPost]
        public JsonResult EditOrder(EditOrderViewModel order)
        {
            using (var db = new CobraOnboarddbEntities())
            {
                var orderdetails = db.OrderHeaders.Where(x => x.Id == order.Id).FirstOrDefault();
                {
                    orderdetails.PersonId = order.Ord.CustomerId;
                }
                var orderheader = db.OrderDetails.Where(x => x.OderId == order.Id).FirstOrDefault();
                {
                    orderheader.ProductId = order.Ord.ProductId;
                }
                db.SaveChanges();

            }
                return new JsonResult();
        }
        [HttpPost]
        public JsonResult DeleteOrder(string orderId)
        {
            using(var db = new CobraOnboarddbEntities())
            {
                int OrdId = Convert.ToInt16(orderId);
                var orderheaders = db.OrderHeaders.Where(x => x.Id == OrdId).FirstOrDefault();
                db.OrderHeaders.Remove(orderheaders);
                var orderdetails = db.OrderDetails.Where(x => x.OderId == OrdId).FirstOrDefault();
                db.OrderDetails.Remove(orderdetails);
                db.SaveChanges();
            }
            
            return new JsonResult();
        }
    }
}