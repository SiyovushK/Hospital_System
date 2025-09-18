using Domain.DTOs.ReportDTO;
using Domain.Filter;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController(ReportService service) : ControllerBase
{
    [HttpGet("hospital-report")]
    public async Task<ActionResult<HospitalReportDto>> GetHospitalReport([FromQuery] GetReportFilter reportFilter)
    {
        var response = await service.GetHospitalReportAsync(reportFilter);
        return StatusCode(response.StatusCode, response);
    }
}