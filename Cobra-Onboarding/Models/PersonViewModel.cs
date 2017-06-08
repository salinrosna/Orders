using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cobra_Onboarding.Models
{
    public class PersonViewModel
    {
       
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
    }
    public class EditPersonViewModel
    {
        public int Id { get; set; }
        public PersonViewModel Pers { get; set; }
    }
}