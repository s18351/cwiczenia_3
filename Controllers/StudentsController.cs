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
            try
            {
                if (!int.TryParse(id, out _))
                {
                    return BadRequest("Index must be number");
                }
                return Ok(Database.GetAll().First(a => a.indexNumber == id));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Not found");
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            try
            {
                if (!int.TryParse(student.indexNumber, out _)) return BadRequest("Index must be number");
                if (Database.GetAll().Any(a => a.indexNumber == student.indexNumber)) return BadRequest("Student already exist");

                Database.SaveAll(Database.GetAll().Append(student));
                return Ok(student); 
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected error occured");
            }
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
        public IActionResult Delete(int id)
        {
            try
            {
                IEnumerable<Student> students = Database.GetAll();

                if (!students.Any(a => int.Parse(a.indexNumber) == id)) return BadRequest("Student not found");
                Database.SaveAll(students.Where(a => int.Parse(a.indexNumber) != id));
                return Ok("Student removed");
            }
            catch (Exception e)
            {
                return BadRequest("Incorrect index");
            }
        }
    }
}
