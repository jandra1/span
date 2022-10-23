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
import { CreateCompanyModel } from '../models/create-company.model';
import { CompanyService } from '../services/company.service';


@Component({
  templateUrl: 'create-company.component.html',
  styleUrls: ['./create-company.component.scss'],
  selector: 'nz-demo-grid-gutter',
}) 

export class CreateCompanyComponent implements OnInit {

  constructor(
    private fb: UntypedFormBuilder,
    private companyService: CompanyService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
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
    });
  
   
  
    ngOnInit(): void {
      //throw new Error('Method not implemented.');
    }
  
    onSubmit(): void {
      // if (this.fg.invalid) {
      //   this.fg.markAllAsTouched();
      //   return;
      // }
  
      const createModel: CreateCompanyModel = {
        name: this.fg.value.name!,
        taxId: this.fg.value.taxId,
        vatId: this.fg.value.vatId,
        phone: this.fg.value.phone!,
        address: this.fg.value.address!,
        postalCode: this.fg.value.postalCode!,
        city: this.fg.value.city!,
        state: this.fg.value.state!,
        correspondencePersonId: this.fg.value.correspondencePersonId!
      };

      this.companyService.createCompany(createModel).subscribe((_) => {
       console.log(createModel)
       console.log("Company succesfully created");
      });
    }
  }


  
  