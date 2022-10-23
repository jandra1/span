import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateUserModel } from '../models/create-user.model';
import { UserService } from '../services/user.service';





@Component({
    templateUrl: 'create-user.component.html',
    styleUrls: ['./create-user.component.scss'],
  })
  export class CreateUserComponent implements OnInit {
    constructor(
      private fb: FormBuilder,
      private userService: UserService,
      private router: Router,
      private activatedRoute: ActivatedRoute,
    ) {};

    
    fg = this.fb.group({
      firstName: ['', Validators.required],
      lastName:  ['', Validators.required],
      //password:  ['', Validators.required],
      email:  ['',Validators.required],
      roleId: new FormControl<number | null>(null),
      phone:  [''],
      address: [''],
      postalCode: new FormControl<number | null>(null),
      city:  [''],
      sex:  [null],
      defaultLanguage: [''],
      dateCreated:  new FormControl<Date | null>(null),
      isActive:  new FormControl<boolean | null>(null),
      termsAccepted:  new FormControl<boolean | null>(null),
      company: [''],
      cultureObjectId:  new FormControl<number | null>(null),
      externalId: ['']
    });
      
    ngOnInit(): void {
      
    }

    onSubmit(): void {
      const createModel: CreateUserModel = {
        firstName: this.fg.value.firstName!,
        lastName: this.fg.value.lastName!,
        email: this.fg.value.email!,
        roleId: this.fg.value.roleId!,
        phone: this.fg.value.phone!,
        address: this.fg.value.address!,
        postalCode: this.fg.value.postalCode!,
        city: this.fg.value.city!,
        sex: this.fg.value.sex!,
        defaultLanguage: this.fg.value.defaultLanguage!,
        dateCreated: this.fg.value.dateCreated!,
        isActive: this.fg.value.isActive!,
        termsAccepted: this.fg.value.termsAccepted!,
        company: this.fg.value.company!,
        cultureObjectId: this.fg.value.cultureObjectId!,

        externalId: this.fg.value.externalId!,
      };
      
      
       
      
      this.userService.createUser(createModel).subscribe((_) => {
      //  console.log(createModel)
        console.log('User successfully created in database!!!!!!!!!!!!!!!!!!');
       
      });

    }
  }

