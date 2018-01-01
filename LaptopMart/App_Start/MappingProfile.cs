using AutoMapper;
using LaptopMart.Dtos;
using LaptopMart.Models;
using LaptopMart.ViewModels;

namespace LaptopMart.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //  Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDto>());
            //   Mapper.Initialize(cfg => cfg.CreateMap<CategoryDto, Category>());
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>());

            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDto, Category>());

            var config3 = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryFormViewModel>());

            var config4 = new MapperConfiguration(cfg => cfg.CreateMap<CategoryFormViewModel, Category>());

            var config5 = new MapperConfiguration(cfg => cfg.CreateMap<Supplier, SupplierDto>());

            var config6 = new MapperConfiguration(cfg => cfg.CreateMap<SupplierDto, Supplier>());

            var config7 = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>());

            var config8 = new MapperConfiguration(cfg => cfg.CreateMap<ProductDto, Product>());
            
            CreateMap<ProductFormViewModel, Product>()
                .ForMember(m => m.Category,opt => opt.Ignore())
                .ForMember(m => m.Supplier, opt => opt.Ignore())
                .ForMember(m => m.SupplierName, opt => opt.Ignore());

            var config10 = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductFormViewModel>());


        }
    }
        

}
