import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { UserService } from "../_services/user.service";

@Injectable({ providedIn: 'root' })
export class Auth implements CanActivate {
  constructor(
    private router: Router,
    private userService: UserService
  ) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = this.userService.getUser;
    if (user == undefined) {
      return true;
    }
    this.router.navigate(['login']);
    return false;
  }
}
