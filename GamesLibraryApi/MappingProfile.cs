using AutoMapper;
using GamesLibraryApi.Dto.Games;
using GamesLibraryApi.Dto.Users;
using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Models.Users;

namespace GamesLibraryApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
            CreateMap<Game, ShowGameDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Tag, TagDto>();
            CreateMap<TagDto, Tag>();
            CreateMap<Media, MediaDto>();
            CreateMap<MediaDto, Media>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Review, ShowReviewDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<ShowUserDto, User>();
            CreateMap<User, ShowUsersDto>();
            CreateMap<User, ShowUserDto>();
            CreateMap<UserGamePurchase, UserGamePurchaseDto>();
        }
    }
}
