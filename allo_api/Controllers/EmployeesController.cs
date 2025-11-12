using Dapper;
using Demo.API.Dto;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
namespace Demo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
  public EmployeesController()
  {

  }

  [HttpGet]
  public async Task<IActionResult> GetEmployees()
  {
    using (IDbConnection dbConnection = CreateConnection())
    {
      if (dbConnection.State != ConnectionState.Open)
        dbConnection.Open();

      var sql = "SELECT * FROM employee;";

      var employees = await dbConnection.QueryAsync<Employee>(sql);

      return Ok(employees);
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetEmployee(int id)
  {

    try
    {
      ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

      using (IDbConnection dbConnection = CreateConnection())
      {
        if (dbConnection.State != ConnectionState.Open)
          dbConnection.Open();

        var sql = "SELECT * FROM employee where id = @id;";

        var employees = await dbConnection.QueryFirstOrDefaultAsync<Employee>(sql, new { id });

        return Ok(employees);
      }
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPost]
  public async Task<IActionResult> CreateEmployee(CreateEmployeeDto employee)
  {
    try
    {
      ArgumentNullException.ThrowIfNull(employee);
      ArgumentException.ThrowIfNullOrEmpty(employee.fullname);
      ArgumentException.ThrowIfNullOrEmpty(employee.address);
      ArgumentOutOfRangeException.ThrowIfNegativeOrZero(employee.age);
      ArgumentNullException.ThrowIfNull(employee.birthday);

      using (IDbConnection dbConnection = CreateConnection())
      {
        if (dbConnection.State != ConnectionState.Open)
          dbConnection.Open();
  
        var sql = $@"
          INSERT INTO employee 
            (fullname, address, age, birthday) 
          VALUES 
            (@fullname, @address, @age, @birthday);";

        try
        {
          await dbConnection.ExecuteAsync(sql, employee);

          return Ok("Employee created successfully");
        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }
      }
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }

  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteEmployee(int id)
  {

    using (IDbConnection dbConnection = CreateConnection())
    {
      if (dbConnection.State != ConnectionState.Open)
        dbConnection.Open();

      var sql = "DELETE FROM employee WHERE id = @id;";

      var affectedRows = await dbConnection.ExecuteAsync(sql, new { id });

      if (affectedRows > 0)
      {
        return Ok("Employee deleted successfully");
      }
      else
      {
        return NotFound("Employee not found");
      }
    }
  }



  private IDbConnection CreateConnection()
  {
    return new MySqlConnection("server=localhost;port=3306;database=dapperdemodb;user=root;password=;");
  }


}