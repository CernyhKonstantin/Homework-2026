using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_13._07._2026.Controllers;

[Authorize]

[ApiController]

[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

}