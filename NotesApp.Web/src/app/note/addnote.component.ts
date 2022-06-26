import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NoteService } from '../_services/note.service';

@Component({ templateUrl: 'addnote.component.html' })
export class AddNoteComponent implements OnInit {
  createNoteForm: FormGroup;
  NoteTypes: any = ['Regular Note', 'Reminder', 'Todo', 'Bookmark'];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private noteService: NoteService
  ) {}
  ngOnInit(): void {
    this.createNoteForm = this.formBuilder.group({
      noteType: ['', Validators.required],
      noteText: ['', Validators.required, Validators.maxLength(100)],
      reminderOrDueDate: [null],
      isComplete: [false],
    });
  }

  get getFormFields() {
    return this.createNoteForm.controls;
  }

  changeNoteType(e: any) {
    this.noteTypeValue.setValue(e.target.value, {
      onlySelf: true,
    });
  }

  get noteTypeValue() {
    return this.createNoteForm.get('noteType');
  }

  btnOnSubmit() {
    console.info('--> AddNote.Component - btnOnSubmit Clicked');
    console.info(this.createNoteForm.value);

    if (this.createNoteForm.invalid) {
      console.info('--> invalid form');
      alert('Invalid form');
      return;
    }
    console.info('--> valid form')

    this.noteService.createNote(this.createNoteForm.value)
      .pipe()
      .subscribe({
        next: () =>{
          this.router.navigate(['notes']);
        },
        error: error => {
          console.error('--> AddNote.Component - btnOnSubmit Clicked error:');
          console.error(error);
        }
      })
  }
}
