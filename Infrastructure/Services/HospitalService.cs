using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs.HospitalDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;

namespace Infrastructure.Services;

public class HospitalService(DataContext context, IMapper mapper)
{
    public async Task<PagedResponse<List<GetHospitalDTO>>> GetAllAsync(HospitalFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var query = context.Hospitals
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(p => p.Name.Contains(filter.Name));

        var data = await query
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ProjectTo<GetHospitalDTO>(mapper.ConfigurationProvider)
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return new PagedResponse<List<GetHospitalDTO>>(data, validFilter.PageNumber, validFilter.PageSize, totalCount);
    }

    public async Task<Response<GetHospitalDTO>> GetById(string id)
    {
        var hospital = await context.Hospitals
            .AsNoTracking()
            .ProjectTo<GetHospitalDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(h => h.RegistrationNumber == id);
        if (hospital == null)
            return new Response<GetHospitalDTO>(HttpStatusCode.NotFound, "Hospital not found");

        return new Response<GetHospitalDTO>(hospital);
    }
    
    public async Task<Response<GetHospitalDTO>> CreateAsync(CreateHospitalDTO hospitalDTO)
    {
        var hospital = mapper.Map<Hospital>(hospitalDTO);
        hospital.RegistrationNumber = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10);

        await context.Hospitals.AddAsync(hospital);
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<GetHospitalDTO>(mapper.Map<GetHospitalDTO>(hospital))
            : new Response<GetHospitalDTO>(HttpStatusCode.InternalServerError, "Failed to created");
    }

    public async Task<Response<GetHospitalDTO>> UpdateAsync(string id, UpdateHospitalDTO hospital)
    {
        var existingHospital = await context.Hospitals.FindAsync(id);
        if (existingHospital == null)
            return new Response<GetHospitalDTO>(HttpStatusCode.NotFound, "Hospital not found");

        mapper.Map(hospital, existingHospital);

        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<GetHospitalDTO>(mapper.Map<GetHospitalDTO>(existingHospital))
            : new Response<GetHospitalDTO>(HttpStatusCode.InternalServerError, "Failed to update hospital");
    }
    
    public async Task<Response<string>> DeleteAsync(string id)
    {
        var hospital = await context.Hospitals.FindAsync(id);
        if (hospital == null)
            return new Response<string>(HttpStatusCode.NotFound, "Hospital not found");

        hospital.IsDeleted = true;
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>("Hospital deleted successfully")
            : new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete hospital");
    }
}