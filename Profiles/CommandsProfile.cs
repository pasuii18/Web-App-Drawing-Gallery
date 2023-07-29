using AutoMapper;
using MinAPI.Dtos;
using MinAPI.Models;

namespace MinAPI.Profiles
{
    public class TableRowProfile : Profile
    {
        public TableRowProfile()
        {
            // Source -> Target
            CreateMap<TableRow, TableRowReadDto>();
            CreateMap<TableRowCreateDto, TableRow>();
            CreateMap<TableRowUpdateDto, TableRow>();

            CreateMap<PostRow, PostRowReadDto>();
            CreateMap<PostRowCreateDto, PostRow>();
            CreateMap<PostRowUpdateDto, PostRow>();
        }
    }
}