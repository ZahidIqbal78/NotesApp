using AutoMapper;
using NotesApp.API.Data;
using NotesApp.API.DTOs.Note;

namespace NotesApp.API.Services.Note
{
    public class NoteService : INoteService
    {
        private readonly NotesAppDbContext context;
        private readonly IMapper mapper;
        public NoteService(NotesAppDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddNote(DomainModels.Note note)
        {
            this.context.Notes.Add(note);
            this.context.SaveChanges();
        }

        public DomainModels.Note? GetNoteById(int applicationUserId, int noteId)
        {
            return this.context.Notes.SingleOrDefault(x => x.Id == noteId && x.ApplicationUserId == applicationUserId);
        }

        public IEnumerable<DomainModels.Note> GetNotes(int applicationUserId)
        {
            return this.context.Notes.Where(x=>x.ApplicationUserId == applicationUserId).OrderByDescending(x=>x.CreatedAt);
        }
    }
}
