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
    public class DatesController : ApiController
    {
        private List<VoteItem> dates = new List<VoteItem>
        {
            new VoteItem{id=1,  name="2018-06-06", votedNumber=0 },
            new VoteItem{id=2,  name="2018-06-07", votedNumber=0 },
            new VoteItem{id=3,  name="2018-06-08", votedNumber=0 },
            new VoteItem{id=4,  name="2018-06-09", votedNumber=0 },
        };

        static string ConnecttionStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        SqlDataAdapter Myadapter;
        DataSet MyDataSet;

        // GET: api/Dates
        public DataTable Get()
        {
            string SelectCmdText = "select * from dates";
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "dates");
            return (MyDataSet.Tables["dates"]);
        }

        // GET: api/Dates/5
        public VoteItem Get(int id)
        {
            string SelectCmdText = String.Format("select * from dates where id={0}", id);
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "dates");
            VoteItem voteItem = new VoteItem();
            voteItem.id = (int)MyDataSet.Tables["dates"].Rows[0]["id"];
            voteItem.name = (string)MyDataSet.Tables["dates"].Rows[0]["name"];
            voteItem.votedNumber = (int)MyDataSet.Tables["dates"].Rows[0]["votedNumber"];
            return voteItem;
        }

        // POST: api/Dates
        public VoteItem Post([FromBody]VoteItem date)
        {
            string SelectCmdText = "select * from dates";
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "dates");

            date.id = MyDataSet.Tables["dates"].Rows.Count + 1;
            DataRow NewRow = MyDataSet.Tables["dates"].NewRow();            
            NewRow["id"] = date.id;
            NewRow["name"] = date.name;
            NewRow["votedNumber"] = date.votedNumber;
            MyDataSet.Tables["dates"].Rows.Add(NewRow);

            SqlCommandBuilder Builder = new SqlCommandBuilder(Myadapter);
            Myadapter.Update(MyDataSet, "dates");
            return date;
        }

        // PUT: api/Dates
        public HttpResponseMessage Put([FromBody]VoteItem date)
        {
            string SelectCmdText = String.Format("select * from dates where id={0}", date.id);
            Myadapter = new SqlDataAdapter(SelectCmdText, ConnecttionStr);
            MyDataSet = new DataSet();
            Myadapter.Fill(MyDataSet, "dates");

            MyDataSet.Tables["dates"].Rows[0]["id"] = date.id;
            MyDataSet.Tables["dates"].Rows[0]["name"] = date.name;
            MyDataSet.Tables["dates"].Rows[0]["votedNumber"] = date.votedNumber;

            SqlCommandBuilder Builder = new SqlCommandBuilder(Myadapter);
            int changedRows = Myadapter.Update(MyDataSet, "dates");            
            if (changedRows >= 1)
            {       
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
