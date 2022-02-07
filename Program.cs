
using System;
using System.Data.SqlClient;
using System.Data;
namespace DisconnectedADODemos
{
    class Program
    {
        static SqlConnection connection;
        static SqlCommand command;
        static SqlDataAdapter ada;
        static DataSet ds;
        static string connectionString = "data source=LAPTOP-53S2KQS8;initial catalog=CGIDb;integrated security=true";
        static SqlCommandBuilder cbd;
        static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        static void FillData()
        {    if (connection.State == ConnectionState.Closed)
            {
                connection = GetConnection();
                connection.Open();
            }
           
             command = new SqlCommand("Select * from article", connection);
            //command = new SqlCommand("GetArticles");
            //command.CommandType = CommandType.StoredProcedure;
            ada = new SqlDataAdapter(command);
            ds = new DataSet();
            cbd = new SqlCommandBuilder(ada);
            ada.Fill(ds,"article");
        }
      static void GetArticles()
        {

            foreach(DataRow dr in ds.Tables["article"].Rows)
            {
                foreach(DataColumn dc in ds.Tables["article"].Columns)
                {
                    Console.WriteLine(dr[dc].ToString() + " "); ;
                }
            }

        }
        static void InsertArticle()
        {
            DataRow dr = ds.Tables["article"].NewRow();
            Console.WriteLine("Enter Title");
            dr["title"] = Console.ReadLine();
            Console.WriteLine("Enter Body");
            dr["body"] = Console.ReadLine();
            Console.WriteLine("Enter Publish Date");
            dr[3] = Convert.ToDateTime(Console.ReadLine());
            ds.Tables["article"].Rows.Add(dr);

            ada.Update(ds, "article");
        }
        static void UpdateArticle()
        {
            foreach(DataRow dr in ds.Tables["article"].Rows)
            {
                foreach(DataColumn dc in ds.Tables["article"].Columns)
                {
                    if(dr["title"].ToString()=="title3")
                    {
                        dr["body"] = "Changed Body";
                        dr["PublishedDate"] = Convert.ToDateTime("09/08/2021");
                    }
                }
                ada.Update(ds, "article");
            }
        }

        static void DeleteArticle()
        {
            for(int i=0;i<ds.Tables["article"].Rows.Count;i++)
            { 
                     if (ds.Tables["article"].Rows[i]["title"].ToString() == "title3")
                    {
                    ds.Tables["article"].Rows[i].Delete();
                    }
                }
            ada.Update(ds, "article");
            }
         
        static void Main(string[] args)
        {
            FillData();
            GetArticles();
            InsertArticle();
            UpdateArticle();
            DeleteArticle();
        }
    }
}
