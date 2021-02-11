namespace Tasks.BLL.DTOs
{
    public class AdditionalTaskEmployeeDTO
    {
        public int AdditionalTaskId { get; set; }
        public int EmployeeId { get; set; }

        public AdditionalTaskDTO AdditionalTask { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}
