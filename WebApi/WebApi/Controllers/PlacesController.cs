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
    public class PlacesController : ApiController
    {
        private List<VoteItem> places = new List<VoteItem>
        {
            new VoteItem{id=1,name="School",votedNumber=0 },
            new VoteItem{id=2,name="KTV",   votedNumber=0 },
            new VoteItem{id=3,name="Home",  votedNumber=0 },
            new VoteItem{id=4,name="Bar",   votedNumber=0 },
        };

        static string ConnecttionStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        SqlDataAdapter Myadapter;
        DataSet MyDataSet;

        // GET: api/Places
        public DataTable Get()
        {
            string SelectCmdText = "select * from places";
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "places");
            return (MyDataSet.Tables["places"]);
        }

        // GET: api/Places/5
        public VoteItem Get(int id)
        {
            string SelectCmdText = String.Format("select * from places where id={0}", id);
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "places");
            VoteItem voteItem = new VoteItem();
            voteItem.id = (int)MyDataSet.Tables["places"].Rows[0]["id"];
            voteItem.name = (string)MyDataSet.Tables["places"].Rows[0]["name"];
            voteItem.votedNumber = (int)MyDataSet.Tables["places"].Rows[0]["votedNumber"];
            return voteItem;
        }

        // POST: api/Places
        public VoteItem Post([FromBody]VoteItem place)
        {
            string SelectCmdText = "select * from places";
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "places");

            place.id = MyDataSet.Tables["places"].Rows.Count + 1;
            DataRow NewRow = MyDataSet.Tables["places"].NewRow();            
            NewRow["id"] = place.id;
            NewRow["name"] = place.name;
            NewRow["votedNumber"] = place.votedNumber;
            MyDataSet.Tables["places"].Rows.Add(NewRow);

            SqlCommandBuilder Builder = new SqlCommandBuilder(Myadapter);
            Myadapter.Update(MyDataSet, "places");
            return place;
        }

        // PUT: api/Places
        public HttpResponseMessage Put([FromBody]VoteItem place)
        {
            string SelectCmdText = String.Format("select * from places where id={0}", place.id);
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "places");

            MyDataSet.Tables["places"].Rows[0]["id"] = place.id;
            MyDataSet.Tables["places"].Rows[0]["name"] = place.name;
            MyDataSet.Tables["places"].Rows[0]["votedNumber"] = place.votedNumber;

            SqlCommandBuilder Builder = new SqlCommandBuilder(Myadapter);
            int changedRows = Myadapter.Update(MyDataSet, "places");
            if (changedRows >= 1)
            {
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
