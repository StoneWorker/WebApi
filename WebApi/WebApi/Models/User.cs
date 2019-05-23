using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string selectedDate { get; set; }
        public string selectedPlace { get; set; }
        public bool voted { get; set; }
    }
}