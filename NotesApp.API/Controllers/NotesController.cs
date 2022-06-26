using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.API.DomainModels;
using NotesApp.API.DTOs.Note;
using NotesApp.API.Helpers;
using NotesApp.API.Services.Note;

namespace NotesApp.API.Controllers
{
    [AuthorizeAttributeHelper]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService noteService;
        private readonly IMapper mapper;
        private readonly IValidator<AddNoteRequestDto> addNoteValidator;


        public NotesController(INoteService noteService,
            IMapper mapper,
            IValidator<AddNoteRequestDto> addNoteValidator)
        {
            this.noteService = noteService;
            this.mapper = mapper;
            this.addNoteValidator = addNoteValidator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DomainModels.Note>> GetAll()
        {
            var userId = (int)HttpContext.Items["UserId"];
            return Ok(this.noteService.GetNotes(userId));
        }

        [HttpGet("{noteId}", Name = "GetNoteById")]
        public ActionResult<DomainModels.Note> GetById([FromRoute] int noteId)
        {
            var userId = (int)HttpContext.Items["UserId"];
            return Ok(this.noteService.GetNoteById(userId, noteId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddNoteRequestDto note)
        {
            var validationResult = this.addNoteValidator.Validate(note);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var noteObj = this.mapper.Map<DomainModels.Note>(note);
            noteObj.ApplicationUserId = (int)HttpContext.Items["UserId"];
            this.noteService.AddNote(noteObj);
            return CreatedAtRoute("GetNoteById", new { noteId = noteObj.Id }, noteObj);
        }
    }
}
