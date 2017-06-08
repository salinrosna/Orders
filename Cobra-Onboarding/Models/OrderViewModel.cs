using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cobra_Onboarding.Models
{
    public class OrderViewModel
    {
        
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
       
    }
    public class EditOrderViewModel
    {
        public int Id { get; set; }
        public OrderViewModel Ord { get; set; }
    }
}