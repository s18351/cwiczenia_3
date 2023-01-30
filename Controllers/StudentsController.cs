using Microsoft.AspNetCore.Mvc;
using Model;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet]
        public IActionResult Get()
        {
            //C:\Users\jerzy\Desktop\APDB\cwiczenia_2\ConsoleApp1\Data\dane.csv
            return Ok(Database.GetAll());
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(Database.GetAll().First(a => a.indexNumber == id));
        }

        // POST api/<StudentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Student student)      
        {
            try
            {
                if (id != int.Parse(student.indexNumber))
                {
                    return BadRequest("Index numbers don't match");
                }

                return Ok(Database.UpdateStudent(student));
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected error occured");
            }
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
