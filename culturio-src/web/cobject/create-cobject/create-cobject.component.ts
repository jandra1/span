import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateCobjectModel } from '../models/create-cobject.model';
import { CobjectService } from '../services/cobject.service';
import { forkJoin } from 'rxjs';
import { DropdownDto } from 'src/app/users/models/dropdownDto';
import { UserService } from 'src/app/users/services/user.service';


@Component({
    templateUrl: 'create-cobject.component.html',
    styleUrls: ['./create-cobject.component.scss'],

  })
  export class CreateCobjectComponent implements OnInit {

    constructor(
      private fb: FormBuilder,
      private cobjectService: CobjectService,
      private router: Router,
      private activatedRoute: ActivatedRoute,
      private userService:UserService

    ) {};

    fg = this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      postalCode: new FormControl<number | null>(null),
      city: ['', Validators.required],
      state: ['', Validators.required],
      workingHours: ['', Validators.required],
      latitude: new FormControl<number | null>(null),
      longitude: new FormControl<number | null>(null),
      cultureObjectTypeId: new FormControl<number | null>(null),
      notes:['', Validators.required],
      responsiblePersonId:new FormControl<number | null>(null),
      cultureObjectCompanyId:new FormControl<number | null>(null),
      cultureObjectType : new FormControl<number | null>(null)
    });

    ngOnInit(): void {
      
    }

  onSubmit(): void{

    const createModel: CreateCobjectModel = {
      name: this.fg.value.name!,
      phone: this.fg.value.phone!,
      address: this.fg.value.address!,
      postalCode: this.fg.value.postalCode!,
      city: this.fg.value.city!,
      state: this.fg.value.state!,
      workingHours: this.fg.value.workingHours!,
      latitude: this.fg.value.latitude!,
      longitude: this.fg.value.latitude!,
      cultureObjectTypeId: this.fg.value.cultureObjectType!,
      notes: this.fg.value.notes!,
      responsiblePersonId: this.fg.value.responsiblePersonId!,
      cultureObjectCompanyId: this.fg.value.cultureObjectCompanyId!,
      
    };

    this.cobjectService.createCobject(createModel).subscribe((_) => {
     
      console.log('Culture object successfully created');
      this.router.navigate(['../..'], { relativeTo: this.activatedRoute });
    });

    
  }
}
