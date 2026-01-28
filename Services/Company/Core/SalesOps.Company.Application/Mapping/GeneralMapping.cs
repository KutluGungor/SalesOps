using AutoMapper;
using SalesOps.Company.Application.Dtos.BranchDtos;
using SalesOps.Company.Application.Dtos.OrganizationDtos;
using SalesOps.Company.Domain.Entity;

namespace SalesOps.Company.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Organization, ResultOrganizationDto>().ReverseMap();
        CreateMap<Organization, CreateOrganizationDto>().ReverseMap();
        CreateMap<Organization, UpdateOrganizationDto>().ReverseMap();
        CreateMap<Organization, ResultOrganizationWithBranchesDto>().ReverseMap();

        CreateMap<Branch, ResultBranchDto>().ReverseMap();
        CreateMap<Branch, CreateBranchDto>().ReverseMap();
        CreateMap<Branch, UpdateBranchDto>().ReverseMap();
    }
}