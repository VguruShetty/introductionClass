namespace WebApplicationAPI.DTo
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
    }
}
