using System.Data;
using EmployeePracticeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeePracticeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("DBConnection");
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spGetAllEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeModel employee = new EmployeeModel();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.EmployeeName = rdr["EmployeeName"].ToString();
                    employee.Email = rdr["Email"].ToString();
                    employee.Age = Convert.ToInt32(rdr["Age"]);
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    employee.City = rdr["City"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Gender = rdr["Gender"].ToString();

                    employeeList.Add(employee);
                }
                connection.Close();
            }
            return View(employeeList);
            //return Json(new { data = employeeList });
        }


        [HttpPost]
        public JsonResult AddEmployee([FromBody] EmployeeModel model)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spAddEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Age", model.Age);
                command.Parameters.AddWithValue("@Salary", model.Salary);
                command.Parameters.AddWithValue("@City", model.City);
                command.Parameters.AddWithValue("@Department", model.Department);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();
                if (result > 0)
                {
                    return new JsonResult("Data Added Successfully");
                }
                else
                {
                    return new JsonResult("Failed to Add Data");
                }
            }


        }


        [HttpPost]
        public JsonResult EditEmployee([FromBody] EmployeeModel employee)
        {
            if (employee == null || employee.EmployeeId == 0)
            {
                return new JsonResult("Invalid data.");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spUpdateEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(@"EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@City", employee.City);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();
                return new JsonResult(result > 0 ? "Employee updated successfully" : "Failed to update employee");

            }


        }


        [HttpGet]
        public JsonResult GetDataById(int id)
        {
            EmployeeModel employee = new EmployeeModel();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
          
                SqlCommand command = new SqlCommand("spGetEmployeeById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        employee.EmployeeId = reader.GetInt32(0);
                        employee.EmployeeName = reader.GetString(1);
                        employee.Email = reader.GetString(2);
                        employee.Age = reader.GetInt32(3);
                        employee.Salary = reader.GetInt32(4);
                        employee.City = reader.GetString(5);
                        employee.Department = reader.GetString(6);
                        employee.Gender = reader.GetString(7);
                    }
                   
                }
                connection.Close();
                return new JsonResult(employee);
            }

        }

        public JsonResult DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("spDeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", id);

                connection.Open();
                int res = command.ExecuteNonQuery();
                connection.Close();
                return Json(new { success = true, message = "Deleted successfully!" });
            }
        }
        




    }
}
