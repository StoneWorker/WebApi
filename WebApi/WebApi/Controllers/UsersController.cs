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
        /*
       private List<User> users = new List<User>
        {
            new User{id=1,name="Jim123",password="e10adc3949ba59abbe56e057f20f883e",selectedDate="",selectedPlace="",voted=false },
            new User{id=2,name="Jim",   password="e10adc3949ba59abbe56e057f20f883e",selectedDate="",selectedPlace="",voted=false },
            new User{id=3,name="Lily",  password="e10adc3949ba59abbe56e057f20f883e",selectedDate="",selectedPlace="",voted=false },
        };
        */

        static string ConnecttionStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        SqlDataAdapter Myadapter;
        DataSet MyDataSet;

        // GET: api/Users
        public DataTable Get()
        {
            string SelectCmdText = "select id,name,selectedDate,selectedPlace,voted from users";
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "users");
            return (MyDataSet.Tables["users"]);
        }

        // GET: api/Users/2
        public User Get(int id)
        {
            string SelectCmdText = String.Format( "select * from users where id={0}",id );
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "users");
            User user = new User();
            user.id = (int)MyDataSet.Tables["users"].Rows[0]["id"];
            user.name = (string)MyDataSet.Tables["users"].Rows[0]["name"];
            //user.password = (string)MyDataSet.Tables["users"].Rows[0]["password"];
            user.selectedDate = (string)MyDataSet.Tables["users"].Rows[0]["selectedDate"];
            user.selectedPlace = (string)MyDataSet.Tables["users"].Rows[0]["selectedPlace"];
            user.voted = (bool)MyDataSet.Tables["users"].Rows[0]["voted"];
            return user;
        }

        // GET: api/Users/?name=Jim
        public DataTable Get(string name)
        {
            string SelectCmdText = String.Format("select id,name,selectedDate,selectedPlace,voted from users where name='{0}'", name);
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "users");
            return MyDataSet.Tables["users"];
        }


        // POST: api/Users
        //[AllowAnonymousAttribute]
        public object Post([FromBody]string[] user1)
        {
            if (user1[2] == "register")
            {

                string SelectCmdText = "select * from users";
                Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
                MyDataSet = new DataSet();
                Myadapter.Fill(MyDataSet, "users");

                DataRow NewRow = MyDataSet.Tables["users"].NewRow();
                NewRow["id"] = MyDataSet.Tables["users"].Rows.Count + 1; ;
                NewRow["name"] = user1[0];
                NewRow["password"] = user1[1];
                NewRow["selectedDate"] = "";
                NewRow["selectedPlace"] = "";
                NewRow["voted"] = false;
                MyDataSet.Tables["users"].Rows.Add(NewRow);

                SqlCommandBuilder Builder = new SqlCommandBuilder(Myadapter);
                int changedRows = Myadapter.Update(MyDataSet, "users");
                if (changedRows >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string SelectCmdText = String.Format("select * from users where name='{0}' and password='{1}'", user1[0], user1[1]);
                Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
                MyDataSet = new DataSet();
                Myadapter.Fill(MyDataSet, "users");
                if (MyDataSet.Tables["users"].Rows.Count > 0)
                {
                    User user = new User();
                    user.id = (int)MyDataSet.Tables["users"].Rows[0]["id"];
                    user.name = (string)MyDataSet.Tables["users"].Rows[0]["name"];
                    //user.password = (string)MyDataSet.Tables["users"].Rows[0]["password"];
                    user.selectedDate = (string)MyDataSet.Tables["users"].Rows[0]["selectedDate"];
                    user.selectedPlace = (string)MyDataSet.Tables["users"].Rows[0]["selectedPlace"];
                    user.voted = (bool)MyDataSet.Tables["users"].Rows[0]["voted"];
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        // PUT: api/Users
        public HttpResponseMessage Put([FromBody]User user)
        {
            string SelectCmdText = String.Format("select * from users where id={0}", user.id);
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "users");

            MyDataSet.Tables["users"].Rows[0]["id"] = user.id;
            MyDataSet.Tables["users"].Rows[0]["name"] = user.name;
            //MyDataSet.Tables["users"].Rows[0]["password"] = user.password;
            MyDataSet.Tables["users"].Rows[0]["selectedDate"] = user.selectedDate;
            MyDataSet.Tables["users"].Rows[0]["selectedPlace"] = user.selectedPlace;
            MyDataSet.Tables["users"].Rows[0]["voted"] = user.voted;

            SqlCommandBuilder Builder = new SqlCommandBuilder(Myadapter);
            int changedRows = Myadapter.Update(MyDataSet, "users");
            if (changedRows >= 1)
            {
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
