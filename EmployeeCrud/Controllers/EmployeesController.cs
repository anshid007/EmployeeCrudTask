using EmployeeCrud.Dto;
using EmployeeCrud.Models;
using EmployeeCrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public EmployeesController(ApplicationDBContext _context)
        {
            context= _context;
        }

        [HttpGet]
        [Route("getEmployees")]
        public async Task<IActionResult> getEmployees()
        {
            try
            {
                //var employees = await context.Employees.ToListAsync();

                var employees = await context.Employees
            .Include(e => e.department)
            .ToListAsync();

                var employeeDTOs = employees.Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Mobile = e.Mobile,
                    Gender = e.Gender,
                    DepartmentId=e.DepartmentId,
                    DepartmentName = e.department.Name
                }).ToList();

                return Ok(employeeDTOs);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getEmployeeById")]
        public ActionResult<Employee> Getbyid(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound("not found");
                }

                Employee data = context.Employees.FirstOrDefault(s => s.Id == id);

                if (data == null)
                {
                    return NotFound("notfound");
                }

                return Ok(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("InsertEmployee")]
        [HttpPost]
        public async Task<IActionResult> InsertEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");


                context.Employees.Add(new Employee()
                {
                    Name = employee.Name,
                    Email= employee.Email,
                    Mobile= employee.Mobile,
                    Gender= employee.Gender,
                    DepartmentId= employee.DepartmentId,
                    
                });

                context.SaveChanges();


                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdateEmployees")]
        public async Task<IActionResult> UpdateEmployees([FromBody] UpdateDTO employee)
        {
            try
            {
                var entity = context.Employees.Include(e => e.department) .FirstOrDefault(e => e.Id == employee.Id);


                entity.Name = employee.Name;
                entity.Email = employee.Email;
                entity.Mobile = employee.Mobile;
                entity.Gender = employee.Gender;
                entity.DepartmentId = employee.DepartmentId;


                //context.Departments.Update(department);
                context.Entry(entity).State = EntityState.Modified; 
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [Route("DeleteEmployee")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var studentToDelete = await context.Employees.FindAsync(id);
                if (studentToDelete == null)
                {
                    return NotFound();
                }
                context.Employees.Remove(studentToDelete);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //[HttpGet]
        //public List<DropDownItemDTO> GetDropdownData()
        //{
        //    List<DropDownItemDTO> dropdownItems = context.Departments
        //        .Select(x => new DropDownItemDTO
        //        {
        //            Id = x.Id,
        //            Name = x.Name
        //        })
        //        .ToList();

        //    return (dropdownItems);
        //}

        [HttpGet]
        [Route("GetDropdownData")]
        public IActionResult GetDropdownData()
        {
            // Fetch data from the table and return it as JSON
            List<DropDownItemDTO> data = context.Departments.Select(x=> new DropDownItemDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return Ok(data);
        }



    }
}
