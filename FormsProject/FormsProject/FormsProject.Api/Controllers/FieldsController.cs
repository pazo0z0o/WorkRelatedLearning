using FormsProject.Api.Models;
using FormsProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace FormsProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FieldsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FormsRepository _repository;

        public FieldsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new FormsRepository(_configuration.GetConnectionString("Primary"));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var field = _repository.GetFieldById(id);

                if (field == null)
                    return NotFound(id);

                return Ok(field);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] FieldViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = _repository.CreateField(model.Name, model.Value, model.FormId);

                return Created($"api/field/{id}", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] FieldViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingField = _repository.GetFieldById(id);

                if (existingField == null)
                    return NotFound(id);

                var success = _repository.UpdateField(id, model.Name, model.Value);

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
                var existingField = _repository.GetFieldById(id);

                if (existingField == null)
                    return NotFound(id);

                var success = _repository.DeleteField(id);

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
    }
}
