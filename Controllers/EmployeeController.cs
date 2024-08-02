using CRUD_API.Interface;
using CRUD_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CRUD_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployee _employeeService;
        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost("Create/{EmployeeString}")]
        public string CreateEmployee(string EmployeeString)
        {
            try 
            { 
                var EmployeeObject = JsonSerializer.Deserialize<Employee>(EmployeeString);
                var ReturnVal = _employeeService.CreateEmployee(EmployeeObject);
                return ReturnVal;
            }
            catch (Exception ex)
            {
                return "Incorrect Format: Expected JSON";
            }


}

        [HttpGet("Search/{Employee_ID}")]
        public string SearchEmployee(int Employee_ID)
        {
            var ReturnVal = _employeeService.GetEmployeeByID(Employee_ID);
            return ReturnVal;
        }


        [HttpGet("GetAll")]
        public string GetAllEmployees()
        {
            var ReturnVal = _employeeService.GetEmployees();
            return ReturnVal;
        }


        [HttpPut("Update/{ID}")]
        public string UpdateEmployee(int ID, Employee EmployeeDetails)
        {
            var ReturnVal = _employeeService.UpdateEmployee(ID, EmployeeDetails);
            return ReturnVal;
        }

        [HttpDelete("Delete/{ID}")]
        public string DeleteEmployee(int ID)
        {
            var ReturnVal = _employeeService.DeleteEmployee(ID);
            return ReturnVal;
        }

    }
}
