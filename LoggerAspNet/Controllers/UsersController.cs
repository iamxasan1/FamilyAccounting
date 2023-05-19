using LoggerAspNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Globalization;

namespace LoggerAspNet.Controllers;

[Route("users")]
public class UsersController : ControllerBase
{
    //private readonly Logger _logger;
    private readonly ILogger _logger;
    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public void Welcome()
    {
        _logger.LogInformation("users get method colled");
        _logger.LogInformation("users get method colled");
    }

    [Route("getUsers")]
    [HttpGet]
    public string Users()
    {

        xexesalom();
        return "xasan xusan hb";

    }

    [Route("error")]
    [HttpGet]
    public void xexesalom()
    {
        throw new Exception("xexesalom nma gap duuusss"); 
    }

}
