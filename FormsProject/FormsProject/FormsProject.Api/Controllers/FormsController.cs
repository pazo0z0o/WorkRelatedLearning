using FormsProject.Api.Models;
using FormsProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace FormsProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FormsRepository _repository;

        public FormsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new FormsRepository(_configuration.GetConnectionString("Primary"));
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var forms = _repository.GetForms();

                return Ok(forms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var form = _repository.GetForm(id);
                if (form == null)
                    return NotFound(id);

                return Ok(form);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(FormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = _repository.CreateForm(model.Title);

                return Created($"api/forms/{id}", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] FormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)  //ASP.NET core use for validation of the model. It uses the [FromBody] parameter and binds it to the model provided
                    return BadRequest(ModelState);

                var existingForm = _repository.GetForm(id);

                if (existingForm == null)
                    return NotFound(id);

                var success = _repository.UpdateForm(id, model.Title);

                if (success)
                    return Ok();
                else
                    return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var existingForm = _repository.GetForm(id);

                if (existingForm == null)
                    return NotFound(id);

                // Before delete the form we need to delete its fields.
                // We can avoid this foreach operation if we set ON DELETE CASCADE in the database
                // or if we create a query/stored procedure that it will delete all the the fields with specific field id.
                var formFields = _repository.GetFieldsByFormId(id);

                foreach (var field in formFields)
                {
                    var deleted = _repository.DeleteField(field.Id);
                    if (!deleted)
                        return BadRequest("Something went wrong");
                }

                var success = _repository.DeleteForm(id);

                if (success)
                    return Ok();
                else
                    return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}/fields")]
        public IActionResult GetFields([FromRoute] int id)
        {
            try
            {
                var existingForm = _repository.GetForm(id);

                if (existingForm == null)
                    return NotFound(id);

                var fields = _repository.GetFieldsByFormId(id);

                return Ok(fields);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
