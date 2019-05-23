using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class VoteItem
    {
        public int      id { get; set; }
        public string   name { get; set; }
        public int      votedNumber { get; set; }
    }
}