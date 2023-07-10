using EmployeeUI.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeUI.Controllers
{
    public class EmployeesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7072/api");
        private readonly HttpClient _client;

        public EmployeesController()
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

        //[HttpGet]
        //public async Task<JsonResult> getEmployees()
        //{

        //    string data = "";
        //    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employees/getEmployees").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        data = response.Content.ReadAsStringAsync().Result;
        //    }
        //    return Json(data);

        //}

        public IActionResult getEmployees()
        {
            try
            {
                var response =  _client.GetAsync(_client.BaseAddress + "/Employees/getEmployees").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var employeeDTOs = JsonConvert.DeserializeObject<List<EmployeeDTO>>(data);

                    // Process the employeeDTOs as needed

                    return Ok(employeeDTOs);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve employee data");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [HttpGet]
        public async Task<JsonResult> GetDropdownData()
        {

            string data = "";
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employees/GetDropdownData").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }
            return Json(data);

        }


        [HttpPost]
        public async Task<IActionResult> InsertEmployee()
        {
            try
            {
                var requestData = await new StreamReader(Request.Body).ReadToEndAsync();
                var emp = JsonConvert.DeserializeObject<EmployeeDTO>(requestData);
                var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Employees/InsertEmployee", emp);

                return Ok(200);

            }
            catch (Exception ex)
            {
                // Handle exception case
                throw ex;
            }
        }


        [HttpPost]
        public async Task<JsonResult> UpdateEmployees()
        {
            try
            {
                var requestData = await new StreamReader(Request.Body).ReadToEndAsync();
                var department = JsonConvert.DeserializeObject<UpdateDTO>(requestData);
                HttpResponseMessage response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Employees/UpdateEmployees", department);


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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_client.BaseAddress}/Employees/DeleteEmployee?id={id}");

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
