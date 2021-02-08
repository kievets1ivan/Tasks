namespace Tasks.DAL.Entities
{
    public class AdditionalTaskEmployee : BaseEntity
    {
        public int AdditionalTaskId { get; set; }
        public int EmployeeId { get; set; }
    }
}
