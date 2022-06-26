using AutoMapper;
using NotesApp.API.DTOs.Note;

namespace NotesApp.API.Mappers
{
    public class NoteMapping : Profile
    {
        public NoteMapping()
        {
            // AddNoteRequestDto -> Note
            CreateMap<AddNoteRequestDto, DomainModels.Note>();

            //Note -> ReminderStatsResponseDto
            CreateMap<DomainModels.Note, ReminderStatsResponseDto>();

            //Note -> TodoStatsResponseDto
            CreateMap<DomainModels.Note, TodoStatsResponseDto>();
        }
    }
}
