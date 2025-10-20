using Domain.DTOs.ReportDTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController(IReportService reportService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetStatisticsAllTerritories([FromQuery] DateTime dateFrom,[FromQuery] DateTime dateTo)
    {
        var response = await reportService.GetStatisticsAllTerritories(dateFrom, dateTo);
        return Ok(response);
    }

    [HttpGet("firdavsi")]
    public async Task<IActionResult> GetStatisticsFirdavsi(
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo)
    {
        var response = await reportService.GetStatisticsFirdavsi(dateFrom, dateTo);
         return Ok(response);
    }
    [HttpGet("shohmansur")]
    public async Task<IActionResult> GetStatisticsShohmansur(
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo)
    {
        var response = await reportService.GetStatisticsShohmansur(dateFrom, dateTo);
        return Ok(response);
    }

    [HttpGet("sino")]
    public async Task<IActionResult> GetStatisticsSino(
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo)
    {
        var response = await reportService.GetStatisticsSino(dateFrom, dateTo);
         return Ok(response);
    }

    [HttpGet("somoni")]
    public async Task<IActionResult> GetStatisticsSomoni(
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo)
    {
        var response = await reportService.GetStatisticsSomoni(dateFrom, dateTo);
         return Ok(response);
    }
}
