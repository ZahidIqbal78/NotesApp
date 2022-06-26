import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { UserRegister } from "../_models/User/userregister.model";
import { UserService } from "../_services/user.service";

@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.required, Validators.email],
      name: ['', Validators.required, Validators.maxLength(100)],
      password: ['', Validators.required],
      dateOfBirth: ['', Validators.required]
    })
  }

  get getFormFields() {
    return this.registerForm.controls;
  }

  btnOnSubmit() {
    console.info('--> Register.Component - btnOnSubmit Clicked');

    if (this.registerForm.invalid) {
      console.info('--> invalid form');
      return;
    }
    this.userService.register(this.registerForm.value)
      .pipe()
      .subscribe({
        next: () => {
          this.router.navigate([''])
        },
        error: error => {
          console.error('--> Register.Component - btnOnSubmit Clicked error:');
          console.error(error);
        }
      });
  }

}
