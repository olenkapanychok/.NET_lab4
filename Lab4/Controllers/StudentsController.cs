using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        return Ok(DataStore.Students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudent(int id)
    {
        try
        {
            var student = DataStore.GetStudent(id);
            return Ok(student);
        }
        catch (InvalidDataException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateStudent(Student student)
    {
        try
        {
            DataStore.AddStudent(student);
        }
        catch (InvalidDataException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, Student student)
    {
        try
        {
            DataStore.EditStudent(id, student);
            return NoContent();
        }
        catch (InvalidDataException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        try
        {
            DataStore.DeleteStudent(id);
            return NoContent();
        }
        catch (InvalidDataException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}