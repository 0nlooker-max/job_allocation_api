namespace Demo.API.Dto;

public class CreateJobPositionDto
{
    public string name { get; set; } = string.Empty;
    public decimal beginning_salary { get; set; }
}