namespace Demo.API.Models;

public record JobPosition
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public decimal beginning_salary { get; set; }
}