using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
  public class CommandsProfile : Profile
  {
      public CommandsProfile()
      {
                // SourceMemberNamingConvention to tsrget our destination  
              CreateMap<Command, CommandReadDto>();
              CreateMap<CommandCreateDto, Command>();
              CreateMap<CommandUpdateDto, Command>();
              CreateMap<Command, CommandUpdateDto>();
          
      }
  }  
}