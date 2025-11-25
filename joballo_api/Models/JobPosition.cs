namespace joballo_api.Models
{
    public class JobPosition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal BeginningSalary { get; set; }
    }
}