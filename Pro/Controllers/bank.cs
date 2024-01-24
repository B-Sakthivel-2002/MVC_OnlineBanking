using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Pro.Models;

namespace Pro.Controllers
{
    public class bank : Controller
    {
        public IConfiguration Configuration { get; }

        public bank(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            List<NewTable> inventoryList = new List<NewTable>();
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "select * from NewTable";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        NewTable inventory = new NewTable();
                        inventory.AccNo = Convert.ToInt32(sdr["acc_no"]);
                        inventory.Pwd = Convert.ToString(sdr["pwd"]);
                        inventory.Username = Convert.ToString(sdr["Username"]);
                        inventory.Gender = Convert.ToString(sdr["gender"]);
                        inventory.Age = Convert.ToInt32(sdr["age"]);
                        inventory.MobileNo = Convert.ToString(sdr["mobile_no"]);
                        inventory.Balance = Convert.ToInt32(sdr["balance"]);

                        inventoryList.Add(inventory);
                    }
                    connection.Close();
                }

            }
            return View(inventoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewTable inventory)
        {

            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into NewTable (acc_no, pwd, username,gender,age,mobile_no,balance) Values ('{inventory.AccNo}', '{inventory.Pwd}','{inventory.Username}','{inventory.Gender}','{inventory.Age}','{inventory.MobileNo}','{inventory.Balance}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = "Success";
            return RedirectToAction("Index");
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            NewTable inventory = new NewTable();
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from NewTable where acc_no='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        inventory.AccNo = Convert.ToInt32(sdr["acc_no"]);
                        inventory.Pwd = Convert.ToString(sdr["pwd"]);
                        inventory.Username = Convert.ToString(sdr["Username"]);
                        inventory.Gender = Convert.ToString(sdr["gender"]);
                        inventory.Age = Convert.ToInt32(sdr["age"]);
                        inventory.MobileNo = Convert.ToString(sdr["mobile_no"]);
                        inventory.Balance = Convert.ToInt32(sdr["balance"]);

                    }
                    connection.Close();
                }

            }
            return View(inventory);
        }

        [HttpPost]
        public IActionResult Update(NewTable inventory, int id)
        {
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update NewTable SET acc_no='{inventory.AccNo}',pwd='{inventory.Pwd}',Username='{inventory.Username}' ,gender='{inventory.Gender}',age='{inventory.Age}',mobile_no='{inventory.MobileNo}',balance='{inventory.Balance}'where acc_no='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Delete(int id)
        {
            NewTable inventory = new NewTable();
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from NewTable where acc_no='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        inventory.AccNo = Convert.ToInt32(sdr["acc_no"]);
                        inventory.Pwd = Convert.ToString(sdr["pwd"]);
                        inventory.Username = Convert.ToString(sdr["Username"]);
                        inventory.Gender = Convert.ToString(sdr["gender"]);
                        inventory.Age = Convert.ToInt32(sdr["age"]);
                        inventory.MobileNo = Convert.ToString(sdr["mobile_no"]);
                        inventory.Balance = Convert.ToInt32(sdr["balance"]);

                    }
                    connection.Close();
                }

            }
            return View(inventory);
        }


        [HttpPost]
        public IActionResult Delete(NewTable dt,int id)
        {
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete from NewTable where acc_no='{dt.AccNo}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
        }



        [HttpGet]
        public IActionResult Detail(int id)
        {
            NewTable inventory = new NewTable();
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from NewTable where acc_no='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        inventory.AccNo = Convert.ToInt32(sdr["acc_no"]);
                        inventory.Pwd = Convert.ToString(sdr["pwd"]);
                        inventory.Username = Convert.ToString(sdr["Username"]);
                        inventory.Gender = Convert.ToString(sdr["gender"]);
                        inventory.Age = Convert.ToInt32(sdr["age"]);
                        inventory.MobileNo = Convert.ToString(sdr["mobile_no"]);
                        inventory.Balance = Convert.ToInt32(sdr["balance"]);

                    }
                    connection.Close();
                }

            }
            return View(inventory);
        }
    }
}
