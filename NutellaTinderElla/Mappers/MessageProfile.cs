using AutoMapper;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderElla.Data.Dtos.Messaging;
namespace NutellaTinderElla.Mappers
{

    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessagePostDTO>().ReverseMap();

        }
    }
}

