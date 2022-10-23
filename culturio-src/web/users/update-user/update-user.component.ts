
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UpdateUserModel } from '../models/update-user.model';
import { UserDto } from '../models/userDto';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-update-cobject',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})

export class UpdateUserComponent implements OnInit {

  id:number;
  cultureObjectById$:Observable<UserDto[]>;
  
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {};

  fg = this.fb.group({
    id:new FormControl<number | null>(null),
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', Validators.required],
    roleId: new FormControl<number | null>(null),
    phone: ['', Validators.required],
    address: ['', Validators.required],
    postalCode: new FormControl<number | null>(null),
    city:  ['', Validators.required],
    sexId: new FormControl<number | null>(null),
    defaultLanguage:['', Validators.required],
    termsAccepted:new FormControl<boolean | null>(null),
    qRcode:['', Validators.required],
    isActive:new FormControl<boolean | null>(null),
    companyId:new FormControl<number | null>(null),
    cultureObjectId:new FormControl<number | null>(null),
  });

  ngOnInit(): void {
    this.id=this.activatedRoute.snapshot.params['id'];
    this.userService.getUserById(this.id).subscribe(data=>{
      this.fillForm(data);
    })
    

  }

  private fillForm(data:UserDto){
    console.log(data);
    this.fg.setValue({
      id:data.id,
      firstName:data.firstName,
      lastName:data.lastName,
      email:data.email,
      roleId:null,
      phone:data.phone,
      address:data.address,
      postalCode:data.postalCode,
      city: data.city,
      sexId:null,
      defaultLanguage:data.defaultLanguage,
      termsAccepted:data.termsAccepted,
      qRcode: data.qRcode,
      isActive: data.isActive,
      companyId: null,
      cultureObjectId: null,
    })
  }

  onSubmit(): void{

    const updateModel: UpdateUserModel = {
      id:this.id!,
      firstName: this.fg.value.firstName!,
      lastName: this.fg.value.lastName!,
      email: this.fg.value.email!,
      roleId: this.fg.value.roleId!,
      phone: this.fg.value.phone!,
      address: this.fg.value.address!,
      postalCode: this.fg.value.postalCode!,
      city: this.fg.value.city!,
      sexId: this.fg.value.sexId!,
      termsAccepted: this.fg.value.termsAccepted!,
      defaultLanguage: this.fg.value.defaultLanguage!,
      qRcode: this.fg.value.qRcode!,
      isActive: this.fg.value.isActive!,
      companyId: this.fg.value.companyId!,
      cultureObjectId: this.fg.value.cultureObjectId!,
    };

    this.userService.updateUser(updateModel).subscribe((_) => {
      console.log(updateModel)
      console.log('User successfully updated');
      this.router.navigate(['../..'], { relativeTo: this.activatedRoute });
    });
  }
} 