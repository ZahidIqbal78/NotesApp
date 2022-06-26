using FluentValidation;
using NotesApp.API.DTOs.Note;

namespace NotesApp.API.Validators.Note
{
    public class NoteAddRequestValidator : AbstractValidator<AddNoteRequestDto>
    {
        public NoteAddRequestValidator()
        {
            RuleFor(x => x.NoteType).NotEmpty().NotNull();

            //Regular Note
            When(dto => dto.NoteType == "Regular Note", () =>
            {
                RuleFor(x => x.NoteText).NotEmpty().NotNull().MaximumLength(100);
            });

            //Reminder
            When(dto => dto.NoteType == "Reminder", () =>
            {
                RuleFor(x => x.NoteText).NotEmpty().NotNull().MaximumLength(100);
                RuleFor(x => x.ReminderOrDueDate).NotEmpty().NotEmpty();
                RuleFor(x => x)
                    .Must(x => x.ReminderOrDueDate.GetValueOrDefault() >= DateTime.Now.AddSeconds(-1))
                    .WithMessage("Reminder date and time must be greater than current date and time.");
            });

            //Todo
            When(dto => dto.NoteType == "Todo", () =>
            {
                RuleFor(x => x.NoteText).NotEmpty().NotNull().MaximumLength(100);
                RuleFor(x => x.IsComplete).NotNull();
                RuleFor(x => x.ReminderOrDueDate).NotEmpty().NotEmpty();
                RuleFor(x => x)
                    .Must(x => x.ReminderOrDueDate.GetValueOrDefault() >= DateTime.Now.AddSeconds(-1))
                    .WithMessage("Due date and time must be greater than current date and time."); ;
            });

            //Bookmark
            When(dto => dto.NoteType == "Bookmark", () =>
            {
                RuleFor(x => x.NoteText).NotEmpty().NotNull().MaximumLength(100)
                    .Must(uri=> Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .When(x=> !string.IsNullOrEmpty(x.NoteText))
                    .WithMessage("The URI is not valid. Please make sure to include http:// or https:// . (Example Valid Format: http(s)://www.abcd.com)");
            });
        }
    }
}
