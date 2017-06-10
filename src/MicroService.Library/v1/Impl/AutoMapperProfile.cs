using AutoMapper;
using MicroService.Library.Domain;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Impl
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDocument>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
               .ReverseMap();

            CreateMap<Book, BookDocument>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<BookShelf, BookShelfDocument>()
                .ForMember(dest => dest.BookShelfId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<BookOnShelf, BookOnShelfDocument>()
                .ForMember(dest => dest.BookOnShelfId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.BookShelf, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Borrower, BorrowerDocument>()
                .ForMember(dest => dest.BorrowerId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<BookCheckout, BookCheckoutDocument>()
                .ForMember(dest => dest.BookCheckoutId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookOnShelf.BookId))
                .ForMember(dest => dest.BookShelfId, opt => opt.MapFrom(src => src.BookOnShelf.BookShelfId))
                .ForMember(dest => dest.Borrower, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}