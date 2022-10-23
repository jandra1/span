import { Component, OnInit } from '@angular/core';


import {
  AbstractControl,
  FormBuilder,
  FormControl,
  Validators,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormArray

} from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';
import { DropdownDto } from 'src/app/users/models/dropdownDto';
import { CreateCocompanyModel } from '../models/create-cocompany.model';
import { CocompanyService } from '../services/cocompany.service';


@Component({
  templateUrl: 'create-cocompany.component.html',
  styleUrls: ['./create-cocompany.component.scss'],

}) 

export class CreateCocompanyComponent implements OnInit {
  constructor(
    private fb: UntypedFormBuilder,
    private cocompanyService: CocompanyService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

    fg = this.fb.group({
      name: [''],
      taxId: new FormControl<number | null>(null),
      vatId: new FormControl<number | null>(null),
      phone: [''],
      address: [''],
      postalCode: new FormControl<number | null>(null),
      city: [''],
      state: [''],
      correspondencePersonId: new FormControl<number | null>(null),
      companyLogo:  [''],
      iBAN: new FormControl<number | null>(null),
    });

    ngOnInit(): void {
      //throw new Error('Method not implemented.');
    }
  
    onSubmit() : void {
      const createModel: CreateCocompanyModel = {
        name: this.fg.value.name!,
        taxId: this.fg.value.taxId,
        vatId: this.fg.value.vatId,
        phone: this.fg.value.phone!,
        address: this.fg.value.address!,
        postalCode: this.fg.value.postalCode!,
        city: this.fg.value.city!,
        state: this.fg.value.state!,
        correspondencePersonId: this.fg.value.correspondencePersonId!,
        companyLogo: this.fg.value.companyLogo!,
        iBAN: this.fg.value.iBAN!,
      };

      this.cocompanyService.createCocompany(createModel).subscribe((_) => {
        console.log(createModel)
        console.log("Cocompany succesfully created");
       });

    }
  
  }
  
  
  