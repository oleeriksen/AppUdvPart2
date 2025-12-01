using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    [HttpGet]
    public string Ping()
    {
        return "ServerApp - version 1.1 - 2025-12-01";
    }
}