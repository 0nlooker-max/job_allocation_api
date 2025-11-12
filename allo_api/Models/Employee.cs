namespace Demo.API.Models;

public record Employee
{
    public int id { get; set; }
    public string fullname { get; set; } = string.Empty;
    public string address { get; set; } = string.Empty;
    public int age { get; set; }
    public string birthday { get; set; } = string.Empty;  

}