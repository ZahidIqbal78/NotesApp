import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddNote } from '../_models/Note/addnote.model';
import { Note } from '../_models/Note/note.model';

@Injectable({ providedIn: 'root' })
export class NoteService {
  constructor(private http: HttpClient) {}

  getAllNotes() {
    return this.http.get<Note[]>(`${environment.apiUrl}/api/Notes`);
  }

  getNote(noteId: number) {
    return this.http.get<Note>(`${environment.apiUrl}/api/Notes/${noteId}`);
  }

  createNote(noteObj : AddNote)
  {
    return this.http.post(`${environment.apiUrl}/api/Notes`, noteObj );
  }
}
