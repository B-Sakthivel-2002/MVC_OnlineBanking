using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Pro.Models;

namespace Pro.Controllers
{
    public class user : Controller
    {
        public int Amount { get; set; }
        public IConfiguration Configuration { get; }
      
        NewTable inventory = new NewTable();
        public user(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

     
       

        [HttpPost]
        public IActionResult Index(NewTable inventory)
        {
            

            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (var con = new SqlConnection(connectionString))
            {

           

                using (var cmd = new SqlCommand("select dbo.userlogincheck(@accno,@pwd)", con))
                {
                    cmd.Parameters.AddWithValue("@accno", inventory.AccNo);
                    cmd.Parameters.AddWithValue("@pwd", inventory.Pwd);
                

                            con.Open();
                    
                    int c = Convert.ToInt32(cmd.ExecuteScalar());

                    if (c == 1)
                    {

                        Console.WriteLine("welcome User");
                        
                        con.Close();
                        int id = inventory.AccNo;
                        return RedirectToAction("Detail",new {id});
                    }
                    else
                    {

                        
                        Console.WriteLine("Account No or Password is wrong!!!");
                        return RedirectToAction("Index", "Home");

                    }
                }

            }
           
            
        }




        [HttpGet]
        public IActionResult Detail(int id)
        {
            Console.WriteLine(id);
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




        [HttpGet]
        public IActionResult Withdraw(int id)
        {
            NewTable inventory = new NewTable();
            string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Amount = 30;
                return RedirectToAction("Withdraw",  new { id,Amount});

            }
               
        }
        [HttpPost]
        public IActionResult Withdraww(int id,int amount)
        {
           string connectionString = Configuration["ConnectionStrings:AccConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update NewTable SET  set balance=balance-'{amount}' where acc_no='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            return RedirectToAction("Detail");
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

    }
}
