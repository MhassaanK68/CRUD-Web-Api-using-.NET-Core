using CRUD_API.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CRUD_API.Interface
{
    public class EmployeeRepo : IEmployee
    {
        public readonly DatabaseContext _db;
        public EmployeeRepo(DatabaseContext db)
        {
            _db = db;

        }

        public string CreateEmployee(Employee employee)
        {

            if (employee == null)
            {
                return "Employee Cannot Be Null";
            }
            else
            {
                try
                {
                    _db.Add(employee);
                    _db.SaveChanges();
                    return JsonSerializer.Serialize(employee);

                }
                catch (Exception ex)
                {
                    return "Database Error Occured: " + ex.Message;
                }
            }


        }

        public string GetEmployeeByID(int ID)
        {
            if (!_db.Employees.Any(emp => emp.EmployeeId == ID)) 
            {
                return "Invalid Opertaion: ID Doesn't Exist";
            }
            try
            {
                var Employee = _db.Employees.FirstOrDefault(emp => emp.EmployeeId == ID);
                var EmployeeString = JsonSerializer.Serialize(Employee);
                return EmployeeString;
            }
            catch(Exception ex)
            {
                return "Database Error Occured" + ex.Message;
            }
        }

        public string GetEmployees()
        {
            try
            {
                List<Employee> EmployeeList = _db.Employees.ToList();

                if (EmployeeList == null)
                {
                    return "No Records Found";
                }
                else {
                    return JsonSerializer.Serialize(EmployeeList);
                }
            }
            catch (Exception ex)
            {
                return "Database Error Occured " + ex.Message;
            }

            
        }

        public string UpdateEmployee(int ID, Employee EmployeeToChange)
        {
            try
            {
                if (ID == EmployeeToChange.EmployeeId)
                { 
                    _db.Entry(EmployeeToChange).State = EntityState.Modified;
                    _db.SaveChanges();
                    return JsonSerializer.Serialize(EmployeeToChange);
                }
                else
                {
                    return "Invalid Operation: ID Cannot Be 0";
                }
            }
            catch (Exception ex)
            {
                return "Database Error Occured " + ex.Message;
            }
        }

        public string DeleteEmployee(int ID)
        {
            if (ID == 0)
            {
                return "Invalid Operation: ID cannot be 0"; 
            }
            else if (_db.Employees.Any(x => x.EmployeeId == ID))
            {
                try
                {
                    var UserToRemove = _db.Employees.FirstOrDefault(emp => emp.EmployeeId == ID);
                    _db.Remove(UserToRemove);
                    _db.SaveChanges();
                    return JsonSerializer.Serialize(UserToRemove);
                }
                catch (Exception ex)
                {
                    return "Database Error Occured " + ex.Message;
                }
            }
            else
            {
                return "Invalid Parameter: ID Does Not Exist";
            }
            

        }


    }
}
