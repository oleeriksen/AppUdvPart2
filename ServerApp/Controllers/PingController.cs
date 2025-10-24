using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    
    
    [HttpGet]
    public string Ping()
    {
        return "ServerApp - version 0.1 - 24-11-2026";
    }
}