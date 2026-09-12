using System.Security.Claims;
using HW_11._09._2026.DTOs.Address;
using HW_11._09._2026.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_11._09._2026.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/users/me/addresses")]
public class UserAddressController : ControllerBase
{
    private readonly IUserAddressService _service;

    public UserAddressController(IUserAddressService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<UserAddressReadDto>>> GetMyAddresses(CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var addresses = await _service.GetMyAddressesAsync(userId, cancellationToken);
        return Ok(addresses);
    }

    [HttpGet("{addressId:int}")]
    public async Task<ActionResult<UserAddressReadDto>> GetMyAddress(
        int addressId,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var address = await _service.GetMyAddressAsync(userId, addressId, cancellationToken);
        return address is null ? NotFound() : Ok(address);
    }

    [HttpPost]
    public async Task<ActionResult<UserAddressReadDto>> Create(
        [FromBody] CreateUserAddressDto dto,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var address = await _service.CreateAsync(userId, dto, cancellationToken);
        return CreatedAtAction(nameof(GetMyAddress), new { addressId = address.Id }, address);
    }

    [HttpDelete("{addressId:int}")]
    public async Task<IActionResult> Delete(
        int addressId,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var deleted = await _service.DeleteAsync(userId, addressId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private bool TryGetUserId(out int userId)
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out userId);
    }
}
