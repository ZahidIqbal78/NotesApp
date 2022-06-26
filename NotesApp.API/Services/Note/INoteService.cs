using NotesApp.API.DTOs.Note;
using NotesApp.API.DomainModels;

namespace NotesApp.API.Services.Note
{
    public interface INoteService
    {
        void AddNote(NotesApp.API.DomainModels.Note note);
        IEnumerable<NotesApp.API.DomainModels.Note> GetNotes(int applicationUserId);
        NotesApp.API.DomainModels.Note GetNoteById(int applicationUserId, int noteId);
    }
}
