import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { Observable } from 'rxjs';
import { UserDto } from '../users/models/userDto';
import { UserService } from '../users/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  data$: Observable<UserDto>;
  data: UserDto;
  searchValue = '';
  visible = false;

  fg = this.fb.group({
    title: ['', Validators.required],
    firstname: ['', [Validators.required, Validators.maxLength(1024)]],
    lastname: ['', [Validators.required]],
    email: ['', [Validators.required]],
    sex: [''],
    role: ['', [Validators.required]],
    language: ['', [Validators.maxLength(20)]],
    phone: [''],
    address: [''],
    city: [''],
  });
  constructor(private userService: UserService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.getUserById(6);
    this.data$.subscribe((result) => {
      this.data = result;
      this.fg.controls['firstname'].setValue(this.data.firstName);
      this.fg.controls['lastname'].setValue(this.data.lastName);
      this.fg.controls['email'].setValue(this.data.email);
      if (this.data.sexId == 1) {
        this.fg.controls['sex'].setValue('male');
      } else {
        this.fg.controls['sex'].setValue('female');
      }
      this.fg.controls['language'].setValue(this.data.defaultLanguage);
      this.fg.controls['phone'].setValue(this.data.phone);
      this.fg.controls['address'].setValue(this.data.address);
      this.fg.controls['city'].setValue(this.data.city);
    });
  }
  getUserById(id: number) {
    this.data$ = this.userService.getUserById(id);
  }
}
