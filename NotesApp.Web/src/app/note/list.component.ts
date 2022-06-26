import { Component, OnInit } from '@angular/core';
import { Note } from '../_models/Note/note.model';
import { NoteService } from '../_services/note.service';

@Component({ templateUrl: 'list.component.html' })
export class ListNoteComponent implements OnInit {
  notes: Note[];

  constructor(private noteService: NoteService) {}

  ngOnInit(): void {
    this.noteService.getAllNotes()
        .pipe()
        .subscribe((notes) => {
            this.notes = notes;
            console.log(notes);
        })
  }
}
