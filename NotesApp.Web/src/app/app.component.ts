import { Component } from '@angular/core';
import { UserLoginResponse } from './_models/User/userloginresponse.model';
import { UserService } from './_services/user.service';

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NotesApp.Web';
  user: UserLoginResponse;

  constructor(private userSevice: UserService) {
    this.userSevice.userLoginResponse.subscribe(x=> this.user = x)
  }

  logout() {
    this.userSevice.logout();
  }
  
}


