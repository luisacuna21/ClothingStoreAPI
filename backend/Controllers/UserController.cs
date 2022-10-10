using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/users")]
[EnableCors("_myAllowSpecificOrigins")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<User>> Get() => await _userService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        await _userService.InsertAsync(user);
        return CreatedAtAction(nameof(Get), new { user = user.Username }, user);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User userToUpdate)
    {
        var user = await _userService.GetAsync(id);
        if (user is null)
            return NotFound();

        userToUpdate.Id = user.Id;
        await _userService.UpdateAsync(id, userToUpdate);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userService.GetAsync(id);
        if (user is null)
            return NotFound();

        await _userService.DeleteAsync(id);
        return NoContent();
    }

    // [HttpGet("username")]
}