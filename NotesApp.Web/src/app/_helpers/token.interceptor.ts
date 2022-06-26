import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { UserService } from "../_services/user.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private userService: UserService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const user = this.userService.getUser;
    const isLoggedIn = user && user.token;
    const isApiUrl = req.url.startsWith(environment.apiUrl);
    if (isLoggedIn && isApiUrl) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      });
    }
    return next.handle(req);
  }
}
