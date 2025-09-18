using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs.PatientDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PatientService(DataContext context, IMapper mapper)
{
    public async Task<PagedResponse<List<GetPatientDTO>>> GetAllAsync(PatientFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var query = context.Patients
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.HospitalRegistrationNumber))
            query = query.Where(p => p.HospitalRegistrationNumber == filter.HospitalRegistrationNumber);

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(p => p.Name.Contains(filter.Name));

        var data = await query
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ProjectTo<GetPatientDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
            
        var totalCount = await query.CountAsync();

        return new PagedResponse<List<GetPatientDTO>>(data, validFilter.PageNumber, validFilter.PageSize, totalCount);
    }
    public async Task<Response<GetPatientDTO>> GetByIdAsync(int id)
    {
        var patients = await context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        if (patients == null)
            return new Response<GetPatientDTO>(HttpStatusCode.NotFound, "Patient not found");

        var result = mapper.Map<GetPatientDTO>(patients);

        return new Response<GetPatientDTO>(result);
    }

    public async Task<Response<string>> CreateAsync(CreatePatientDTO patientDTO)
    {
        if (await context.Hospitals.AnyAsync(h => h.RegistrationNumber != patientDTO.HospitalRegistrationNumber))
            return new Response<string>(HttpStatusCode.NotFound, "Hospital not found");

        // if (patientDTO.RecordDate.Date != DateTime.Now.Date)
        //     return new Response<string>(HttpStatusCode.BadRequest, "Invalid input");
    
        var patient = mapper.Map<Patient>(patientDTO);
        await context.Patients.AddAsync(patient);
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>("Created patient successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to created");
    }
    public async Task<Response<string>> UpdateAsync(int id, UpdatePatientDTO patient)
    {
        var existingPatient = await context.Patients.FindAsync(id);
        if (existingPatient == null)
            return new Response<string>(HttpStatusCode.NotFound, "Patient not found");

        mapper.Map(existingPatient, patient);

        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>("Patient updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to update patient");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var patient = await context.Patients.FindAsync(id);
        if (patient == null)
            return new Response<string>(HttpStatusCode.NotFound, "Patient not found");

        context.Patients.Remove(patient);
        var result = await context.SaveChangesAsync();
        return result > 0
            ? new Response<string>("Patient deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to delete patient"); 
    }
}

