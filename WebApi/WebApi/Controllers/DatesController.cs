using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DatesController : ApiController
    {
        private List<VoteItem> dates = new List<VoteItem>
        {
            new VoteItem{id=1,  name="2018-06-06", votedNumber=0 },
            new VoteItem{id=2,  name="2018-06-07", votedNumber=0 },
            new VoteItem{id=3,  name="2018-06-08", votedNumber=0 },
            new VoteItem{id=4,  name="2018-06-09", votedNumber=0 },
        };

        // GET: api/Dates
        public IEnumerable<VoteItem> Get()
        {
            return dates;
        }

        // GET: api/Dates/5
        public VoteItem Get(int id)
        {
            return dates.Find(date => date.id == id);
        }

        // POST: api/Dates
        public VoteItem Post([FromBody]VoteItem date)
        {
            date.id = dates.Count + 1;
            dates.Add(date);
            return date;

        }

        // PUT: api/Dates
        public HttpResponseMessage Put([FromBody]VoteItem date)
        {
            int index = dates.FindIndex(date1 => date1.id == date.id);
            if(index >= 0)
            {
                dates[index] = date;
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        

        // DELETE: api/Dates/5
        public void Delete(int id)
        {
        }
    }
}
