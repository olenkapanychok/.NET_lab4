using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers;

[ApiController]
[Route("[controller]")]
public class TeachersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllTeachers()
    {
        return Ok(DataStore.Teachers);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeacher(int id)
    {
        try
        {
            var teacher = DataStore.GetTeacher(id);
            return Ok(teacher);
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
    public IActionResult CreateTeacher(Teacher teacher)
    {
        try
        {
            DataStore.AddTeacher(teacher);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTeacher(int id, Teacher teacher)
    {
        try
        {
            DataStore.EditTeacher(id, teacher);
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
    public IActionResult DeleteTeacher(int id)
    {
        try
        {
            DataStore.DeleteTeacher(id);
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