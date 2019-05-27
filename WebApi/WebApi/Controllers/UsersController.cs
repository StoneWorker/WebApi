using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
       private List<User> users = new List<User>
        {
            new User{id=1,name="Jim123",password="e10adc3949ba59abbe56e057f20f883e",selectedDate="",selectedPlace="",voted=false },
            new User{id=2,name="Jim",   password="e10adc3949ba59abbe56e057f20f883e",selectedDate="",selectedPlace="",voted=false },
            new User{id=3,name="Lily",  password="e10adc3949ba59abbe56e057f20f883e",selectedDate="",selectedPlace="",voted=false },
        };


        static string ConnecttionStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        SqlDataAdapter Myadapter;
        DataSet MyDataSet;

        // GET: api/Users
        public DataTable Get()
        {
            string SelectCmdText = "select * from users";
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "users");
            return (MyDataSet.Tables["users"]);
        }

        // GET: api/Users/2
        public DataRow Get(int id)
        {
            string SelectCmdText = String.Format( "select * from users where id={0}",id );
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "users");
            return MyDataSet.Tables["users"].Rows[0];
        }

        // GET: api/Users/?name=Jim
        public IEnumerable<User> Get(string name)
        {
            return users.FindAll(user => user.name == name);
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
