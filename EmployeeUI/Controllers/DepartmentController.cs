using EmployeeCrud.Models;
using EmployeeCrudApi.Models;
using EmployeeUI.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeUI.Controllers
{
    public class DepartmentController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7072/api");
        private readonly HttpClient _client;

        public DepartmentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> getDepartments()
        {
            try
            {
                string data = "";
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Department/getDepartments").Result;

                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsStringAsync().Result;
                }
                return Json(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }


        [HttpPost]
        public async Task<IActionResult> InsertDepartment()
        {
            try
            {
                var requestData = await new StreamReader(Request.Body).ReadToEndAsync();
                var department = JsonConvert.DeserializeObject<DepartmentDTO>(requestData);
                var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Department/InsertDepartment", department);

                return Ok(200);

            }
            catch (Exception ex)
            {
                // Handle exception case
                throw ex;
            }
        }


        public async Task<JsonResult> UpdateDepartment()
        {
            try
            {
                var requestData = await new StreamReader(Request.Body).ReadToEndAsync();
                var department = JsonConvert.DeserializeObject<DepartmentDTO>(requestData);

                HttpResponseMessage response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Department/UpdateDepartment", department);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return Json(responseData);
                }
                else
                {
                    // handle error case
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                // handle exception case
                return Json(null);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDept(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_client.BaseAddress}/Department/DeleteDept?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    // Return a success status or appropriate response
                    return Ok();
                }
                else
                {
                    // Return an error status or appropriate response
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Return an error status or appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




    }
}
