import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { UserLogin } from "../_models/User/userlogin.model";
import { UserService } from "../_services/user.service";

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  loading = false;
  submitted = false;
  form: FormGroup;
  userLogin: UserLogin;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,

  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required]
    });
  }

  get getFormFields() { return this.form.controls }

  btnOnSubmit() {
    console.info('--> Login.Component - btnOnSubmit Clicked');

    if (this.form.invalid) {
      console.info('--> invalid form');
      return;
    }

    this.userService.login(this.form.value)
      .pipe()
      .subscribe({
        next: () => {
          this.router.navigate(['/dashboard']);
        },
        error: error => {
          console.error(error);
        }
      });
  }
}
