using Dapper;
using Demo.API.Dto;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Demo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobPositionsController : ControllerBase
{
    private readonly string _connectionString;

    public JobPositionsController()
    {
        _connectionString = "server=localhost;port=3306;database=dapperdemodb;user=root;password=;";
    }

    [HttpGet]
    public async Task<IActionResult> GetJobPositions()
    {
        using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();

            var sql = "SELECT * FROM job_position;";

            var jobPositions = await dbConnection.QueryAsync<JobPosition>(sql);

            return Ok(jobPositions);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobPosition(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                var sql = "SELECT * FROM job_position WHERE id = @id;";

                var jobPosition = await dbConnection.QueryFirstOrDefaultAsync<JobPosition>(sql, new { id });

                if (jobPosition == null)
                    return NotFound("Job position not found");

                return Ok(jobPosition);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateJobPosition(CreateJobPositionDto jobPosition)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(jobPosition);
            ArgumentException.ThrowIfNullOrEmpty(jobPosition.name);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                var sql = @"
                    INSERT INTO job_position 
                        (name, beginning_salary) 
                    VALUES 
                        (@name, @beginning_salary);
                    SELECT LAST_INSERT_ID();";

                var id = await dbConnection.QuerySingleAsync<int>(sql, jobPosition);

                return Ok(new { id, message = "Job position created successfully" });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobPosition(int id, UpdateJobPositionDto jobPosition)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            ArgumentNullException.ThrowIfNull(jobPosition);
            ArgumentException.ThrowIfNullOrEmpty(jobPosition.name);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                var sql = @"
                    UPDATE job_position 
                    SET name = @name, beginning_salary = @beginning_salary 
                    WHERE id = @id;";

                var affectedRows = await dbConnection.ExecuteAsync(sql, new { id, jobPosition.name, jobPosition.beginning_salary });

                if (affectedRows == 0)
                    return NotFound("Job position not found");

                return Ok(new { id, message = "Job position updated successfully" });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobPosition(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                var sql = "DELETE FROM job_position WHERE id = @id;";

                var affectedRows = await dbConnection.ExecuteAsync(sql, new { id });

                if (affectedRows == 0)
                    return NotFound("Job position not found");

                return Ok("Job position deleted successfully");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}