using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cobra_Onboarding.Models;


namespace Cobra_Onboarding.Controllers
{
    public class CobraController : Controller
    {
        // GET: Cobra
        [HttpGet]

        public ActionResult Customer()
        {
            return View();
        }
        public JsonResult Customertable()
        {

            using (var db = new CobraOnboarddbEntities())
            {
                var customer = db.People.Where(x=>x.IsActive==true).Select(x => new { x.Id, x.Name, x.Address1, x.Address2, x.City }).ToList();
                return new JsonResult { Data = customer, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        [HttpPost]
        public JsonResult Customer(PersonViewModel person)
        {
            using (var db = new CobraOnboarddbEntities())
            {
                var customer = new Person()
                {
                    Name = person.Name,
                    Address1 = person.Address1,
                    Address2 = person.Address2,
                    City = person.City,
                    IsActive=true
                };
                db.People.Add(customer);
                db.SaveChanges();
                var customerlist = db.People.Select(x => new { x.Id, x.Name, x.Address1, x.Address2, x.City }).ToList();
                return new JsonResult { Data = customerlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
        }

        [HttpPost]
        public JsonResult EditCust(EditPersonViewModel person)
        {
            using (var db = new CobraOnboarddbEntities())
            {
                //int id = Convert.ToInt32(Id);
               
                var editcustomer = db.People.Where(x => x.Id == person.Id).FirstOrDefault();
               
                
                    editcustomer.Name = person.Pers.Name;
                    editcustomer.Address1 = person.Pers.Address1;
                    editcustomer.Address2 = person.Pers.Address2;
                    editcustomer.City = person.Pers.City;
                
                db.SaveChanges();
                var customer = db.People.Select(x => new { x.Id, x.Name, x.Address1, x.Address2, x.City }).ToList();
                return new JsonResult { Data=customer, JsonRequestBehavior=JsonRequestBehavior.AllowGet};
            }
            
        }

       [HttpPost]
       public JsonResult DeleteCustomer(string data)
        {
            using (var db = new CobraOnboarddbEntities())
            {
                int custId = Convert.ToInt16(data);
                var deletecust = db.People.Where(x => x.Id == custId).FirstOrDefault();
                {
                    deletecust.IsActive = false;
                }
                db.SaveChanges();
            }
            return new JsonResult();
        }

    }
}