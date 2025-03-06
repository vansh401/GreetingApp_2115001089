using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;

namespace HelloGreetingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
            _logger.Info("Logger has been integrated");
        }
        /// <summary>
        /// get method to get response from server
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            _logger.Info("");
            //_logger.Info("working");
            responseModel.Success = true;
            responseModel.Message = "Hello To Greeting";
            responseModel.Data = "Hello App";
            return Ok(responseModel);
        }
        /// <summary>
        /// post method to add an entry to server
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post(RequestModel requestModel) {
            ResponseModel<string> responseModel= new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Request recieved Successfully";
            responseModel.Data=$"Key:{requestModel.key}, Value:{requestModel.value}";
            return Ok(responseModel);
        }
        /// <summary>
        /// Put method to update the greeting message
        /// </summary>
        /// <param name="requestModel">Request model containing key and value</param>
        /// <returns>Response model with updated data</returns>
        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Data updated successfully";
            responseModel.Data = $"Updated Key: {requestModel.key}, Updated Value: {requestModel.value}";
            return Ok(responseModel);
        }

        /// <summary>
        /// Patch method to partially update the greeting message
        /// </summary>
        /// <param name="requestModel">Request model with partial data</param>
        /// <returns>Response model with patched data</returns>
        [HttpPatch]
        public IActionResult Patch(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Data partially updated successfully";
            responseModel.Data = $"Patched Key: {requestModel.key}, Patched Value: {requestModel.value}";
            return Ok(responseModel);
        }

        /// <summary>
        /// Delete method to delete the greeting message
        /// </summary>
        /// <param name="key">Key to identify the resource</param>
        /// <returns>Response model with deletion status</returns>
        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = $"Data with Key: {key} deleted successfully";
            responseModel.Data = null;
            return Ok(responseModel);
        }

        /// <summary>
        /// To print Hello! World
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGreeting")]
        public string GetHello()
        {
            return _greetingBL.GetGreet();

        }


        /// <summary>
        /// this method takes input from user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("PostGreet")]
        public IActionResult PostGreeting(UserModel userModel)
        {
            var result = _greetingBL.greeting(userModel);
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greet Message With Name";
            responseModel.Data = result;
            return Ok(responseModel);

        }

        /// <summary>
        /// Take input greet from user and stores into database
        /// </summary>
        /// <param name="greetModel"></param>
        /// <returns></returns>
        [HttpPost("greetmessage")]
        public IActionResult GreetMessage(GreetingModel greetModel)
        {
            var response = new ResponseModel<string>();
            try
            {
                bool isMessageGrret = _greetingBL.GreetMessage(greetModel);
                if (isMessageGrret)
                {
                    response.Success = true;
                    response.Message = "Greet Message!";
                    response.Data = greetModel.ToString();
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greet Message Already Exist.";
                return Conflict(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Get Greeting Message by Id
        /// </summary>
        /// <param name="id">Id of the Greeting Message</param>
        /// <returns>Greeting Message</returns>
        [HttpGet("GetGreetingById/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var response = new ResponseModel<GreetingModel>();
            try
            {
                var result = _greetingBL.GetGreetingById(id);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = "Greeting Message Found";
                    response.Data = result;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greeting Message Not Found";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }


    }
}
