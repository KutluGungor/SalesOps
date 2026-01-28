using AutoMapper;
using SalesOps.Catalog.Application.Dtos.CategoryDtos;
using SalesOps.Catalog.Application.Dtos.ProductDtos;
using SalesOps.Catalog.Domain.Entity;

namespace SalesOps.Catalog.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Product, ResultProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, ResultProductWithCategoryDto>().ReverseMap();

        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
    }
}
