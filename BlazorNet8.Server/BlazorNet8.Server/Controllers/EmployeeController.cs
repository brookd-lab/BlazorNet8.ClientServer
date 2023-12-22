using BlazorNet8.Shared.Models;
using BlazorNet8.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorNet8.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeRepository _repo;

        public EmployeeController(EmployeeRepository repo,
            ILogger<EmployeeController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("/employees")]
        public async Task<IResult> GetAll()
        {
            var _employees = await _repo.GetAll();
            return Results.Ok(_employees);
        }

        [HttpGet("/employees/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var employee = await _repo.GetById(id);
            return Results.Ok(employee); //200
        }

        [HttpPost("/employees/create/")]
        public async Task<IResult> Create(Employee employee)
        {
            await _repo.Create(employee);
            return Results.Created($"/employee/{employee.Id}", employee);
        }

        [HttpPut("/employees/update/")]
        public async Task<IResult> Update(Employee employee)
        {
            await _repo.Update(employee);
            return Results.NoContent(); //204
        }

        [HttpDelete("/employees/delete/{id}")]
        public async Task<IResult> DeleteByID(int id)
        {
            await _repo.DeleteByID(id);
            return Results.NoContent();
        }
    }
}