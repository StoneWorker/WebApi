using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PlacesController : ApiController
    {
        private List<VoteItem> places = new List<VoteItem>
        {
            new VoteItem{id=1,name="School",votedNumber=0 },
            new VoteItem{id=2,name="KTV",   votedNumber=0 },
            new VoteItem{id=3,name="Home",  votedNumber=0 },
            new VoteItem{id=4,name="Bar",   votedNumber=0 },
        };

        // GET: api/Places
        public IEnumerable<VoteItem> Get()
        {
            return places;
        }

        // GET: api/Places/5
        public VoteItem Get(int id)
        {
            return places.Find(place => place.id == id);
        }

        // POST: api/Places
        public VoteItem Post([FromBody]VoteItem place)
        {
            place.id = places.Count + 1;
            places.Add(place);
            return place;
        }

        // PUT: api/Places
        public HttpResponseMessage Put([FromBody]VoteItem place)
        {
            int index = places.FindIndex(place1 => place1.id == place.id);
            if (index >= 0)
            {
                places[index] = place;
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Places/5
        public void Delete(int id)
        {
        }
    }
}
