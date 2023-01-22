using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZebraPrinterSDK.Repositories;

namespace ZebraPrinterSDK.Controllers;
[ApiController]
[Route("print")]
public class PrintController : ControllerBase
{
    private readonly IPrinterRepo printerRepo;
    public PrintController(IPrinterRepo printerRepo)
    {
        this.printerRepo = printerRepo;
    }
    
    [HttpGet]
    public async Task<IActionResult> Print([FromQuery]string? text)
    {
        Console.WriteLine("************"+text!);
        await printerRepo.Print(text!);
        return Ok("************" + text!);
    }
}
