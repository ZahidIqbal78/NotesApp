import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AddNoteComponent } from "./note/addnote.component";
import { ListNoteComponent } from "./note/list.component";
import { LoginComponent } from "./user/login.component";
import { RegisterComponent } from "./user/register.component";
import { Auth } from "./_helpers/auth";

const routes: Routes = [
  { path: '', component: AppComponent, canActivate: [Auth] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent},
  { path: 'notes', component: ListNoteComponent},
  { path: 'notes/add', component: AddNoteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
