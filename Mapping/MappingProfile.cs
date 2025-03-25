using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Products_Management_API.Data;
using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Auth;
using Products_Management_API.Models.DTO.Category;
using Products_Management_API.Models.DTO.Customer;
using Products_Management_API.Models.DTO.Order;
using Products_Management_API.Models.DTO.Product;
using Products_Management_API.Models.DTO.Review;
using Products_Management_API.Models.DTO.Supplier;
using ProductsManagement.Models.Domain;
using System.Security.Claims;

namespace Products_Management_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();

            //CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();


            CreateMap<OrderUpdateDto, Order>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();

            CreateMap<RequestRegisterDto, ApplicationUser>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap(); // Set UserName = Email
            CreateMap<RequestLoginDto, ApplicationUser>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap(); // Set UserName = Email
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<CreateReviewDto, Review>().ReverseMap();
            CreateMap<UpdateReviewDto, Review>().ReverseMap();

            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<CreateSupplierDto, Supplier>().ReverseMap();
            CreateMap<UpdateSupplierDto, Supplier>().ReverseMap();

            CreateMap<IdentityUser, UserDto>().ReverseMap();
            CreateMap<UpdateUserDto, ApplicationUser>().ReverseMap();
            CreateMap<IdentityRole, RoleDto>().ReverseMap();
            CreateMap<IdentityUser, UserUpdateDto>().ReverseMap();

            CreateMap<ClaimsPrincipal, UserDto>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Identity.Name))
           // You can map other properties from ClaimsPrincipal if needed
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.FindFirst(ClaimTypes.Email)));


        }
    }
}
