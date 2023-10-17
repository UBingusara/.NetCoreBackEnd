using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPIDemo.Data;
using ProductAPIDemo.Model;

namespace ProductAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly ProductAPIDemoContext _productAPIDemoContext;

        public EmployeesController(ProductAPIDemoContext ProductAPIDemoContext)
        {
            _productAPIDemoContext = ProductAPIDemoContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _productAPIDemoContext.MyProperty.ToListAsync();

            return Ok(employees);

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _productAPIDemoContext.MyProperty.AddAsync(employeeRequest);
            await _productAPIDemoContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _productAPIDemoContext.MyProperty.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _productAPIDemoContext.MyProperty.FindAsync(id);
            
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _productAPIDemoContext.SaveChangesAsync();

            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _productAPIDemoContext.MyProperty.FindAsync(id);

        if (employee == null)
            {
                return NotFound();
            }

            _productAPIDemoContext.MyProperty.Remove(employee);
            await _productAPIDemoContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FormBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _productAPIDemoContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);
            if (user == null)
                return NotFound(new { Message = "User Not Found"});
        }
    }

}
