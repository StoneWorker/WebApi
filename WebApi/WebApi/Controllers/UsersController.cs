using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
       private List<User> users = new List<User>
        {
            new User{id=1,name="Jim123",password="123456",selectedDate="",selectedPlace="",voted=false },
            new User{id=2,name="Jim",password="123456",selectedDate="",selectedPlace="",voted=false },
            new User{id=3,name="Lily",password="123456",selectedDate="",selectedPlace="",voted=false },
        };

        // GET: api/Users
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET: api/Users/5
        public User Get(int id)
        {
            return users.Find(user => user.id == id);
        }

        // GET: api/Users/?name=Jim
        public User Get(string name)
        {
            return users.Find(user => user.name == name);
        }


        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users
        public HttpResponseMessage Put([FromBody]User user)
        {
            int index = users.FindIndex(user1 => user1.id == user.id);
            if(index >= 0)
            {
                users[index] = user;
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
