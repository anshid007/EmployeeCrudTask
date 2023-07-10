using EmployeeCrud.Dto;
using EmployeeCrud.Models;
using EmployeeCrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Http.Cors;

namespace EmployeeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public DepartmentController(ApplicationDBContext dBContext)
        {
            context = dBContext;
        }
        [HttpGet]
        [Route("demo")]
        public string demo()
        {
            return "HII";
        }

        [HttpGet]
        [Route("getDepartments")]
        public async Task<IActionResult> getDepartments()
        {
            try
            {
                var dept = await context.Departments.ToListAsync();
                return Ok(dept);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        [HttpGet]
        [Route("getDepartmentById")]
        public ActionResult<Department> Getbyid(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound("not found");
                }

                Department deptdata = context.Departments.FirstOrDefault(s => s.Id == id);

                if (deptdata == null)
                {
                    return NotFound("notfound");
                }

                return Ok(deptdata);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("InsertDepartment")]
        public IActionResult InsertDepartment([FromBody] DepartmentDTO department)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                context.Departments.Add(new Department()
                {
                    Code = department.Code,
                    Name = department.Name
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
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDTO department)
        {
            try
            {
                var entity = await context.Departments.FindAsync(department.Id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Code = department.Code;
                entity.Name = department.Name;

                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [Route("DeleteDept")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDept(int id)
        {
            try
            {
                var DeptToDelete = await context.Departments.FindAsync(id);
                if (DeptToDelete == null)
                {
                    return NotFound();
                }
                context.Departments.Remove(DeptToDelete);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


    }
}
