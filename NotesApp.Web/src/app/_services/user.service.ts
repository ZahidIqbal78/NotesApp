import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Router } from '@angular/router';

import { environment } from '../../environments/environment';

import { UserRegister } from '../_models/User/userregister.model';
import { UserLogin } from '../_models/User/userlogin.model';
import { UserLoginResponse } from '../_models/User/userloginresponse.model';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UserService {
  public userLoginResponse: Observable<UserLoginResponse>;
  private userSubject: BehaviorSubject<UserLoginResponse>;

  constructor(
    private router: Router,
    private http: HttpClient
  ) {
    this.userSubject = new BehaviorSubject<UserLoginResponse>(JSON.parse(localStorage.getItem('user') || '{}'));
    this.userLoginResponse = this.userSubject.asObservable();
  }

  login(loginObj: UserLogin) {
    return this.http.post<UserLoginResponse>(`${environment.apiUrl}/api/User/login`, loginObj )
      .pipe(map(userLoginReponseObject => {
        localStorage.setItem('user', JSON.stringify(userLoginReponseObject));
        this.userSubject.next(userLoginReponseObject);
        return userLoginReponseObject;
      }))
  }

  public get getUser(): UserLoginResponse {
    return this.userSubject.value;
  }

  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/login']);
  }

  register(registerObj: UserRegister) {
    return this.http.post(`${environment.apiUrl}/api/User/register`, registerObj );
  }

  //test
  getAllUsers() {
    return this.http.get(`${environment.apiUrl}/api/User`);
  }
}
