namespace Employee.API.Entities
{
    public class EmployeeRolePostModel
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsManagement { get; set; }
        public int RoleId { get; set; }

        public int Id;


    }
}
