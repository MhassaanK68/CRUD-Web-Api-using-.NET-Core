using CRUD_API.Models;

namespace CRUD_API.Interface
{
    public interface IEmployee
    {
        public string GetEmployees();

        public string GetEmployeeByID(int ID);

        public string CreateEmployee(Employee employee);

        public string UpdateEmployee(int ID, Employee employee);

        public string DeleteEmployee(int ID);

    }
}
