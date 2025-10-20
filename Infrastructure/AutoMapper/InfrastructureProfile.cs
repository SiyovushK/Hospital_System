using AutoMapper;
using Domain.DTOs.HospitalDTO;
using Domain.DTOs.PatientDTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Patient, GetPatientDTO>();
        CreateMap<CreatePatientDTO, Patient>();
        CreateMap<UpdatePatientDTO, Patient>();

        CreateMap<Hospital, GetHospitalDTO>();
        CreateMap<CreateHospitalDTO, Hospital>();
        CreateMap<UpdateHospitalDTO, Hospital>();    
    }
}