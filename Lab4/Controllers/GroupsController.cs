using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllGroups()
    {
        return Ok(DataStore.Groups);
    }

    [HttpGet("{id}")]
    public IActionResult GetGroup(int id)
    {
        try
        {
            var group = DataStore.GetGroup(id);
            return Ok(group);
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
    public IActionResult CreateGroup(Group group)
    {
        try
        {
            DataStore.AddGroup(group);
        }
        catch (InvalidDataException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGroup(int id, Group group)
    {
        try
        {
            DataStore.EditGroup(id, group);
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
    public IActionResult DeleteGroup(int id)
    {
        try
        {
            DataStore.DeleteGroup(id);
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