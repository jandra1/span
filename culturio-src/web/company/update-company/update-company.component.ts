import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {  Observable } from 'rxjs';
import { CompanyDto } from '../models/companyDto';
import { UpdateCompanyModel } from '../models/update-company.model';
import { CompanyService } from '../services/company.service';


@Component({
  selector: 'app-update-cobject',
  templateUrl: './update-company.component.html',
  styleUrls: ['./update-company.component.scss']
})
export class UpdateCompanyComponent implements OnInit {

  id:number;
  
  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {};

  fg = this.fb.group({
    id:new FormControl<number | null>(null),
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
    this.id=this.activatedRoute.snapshot.params['id'];
    this.companyService.getCompanyById(this.id).subscribe(data=>{
      this.fillForm(data);
    })
    

  }

  private fillForm(data:CompanyDto){
    this.fg.setValue({
      id:data.id,
      name: data.name,
      taxId: data.taxId,
      vatId: data.vatId,
      phone: data.phone,
      address: data.address,
      postalCode: data.postalCode,
      city: data.city,
      state: data.city,
      correspondencePersonId: null,
    })
  }

  onSubmit(): void{

    const updateModel: UpdateCompanyModel = {
      id:this.id!,
      name: this.fg.value.name!,
      taxId: this.fg.value.taxId!,
      vatId: this.fg.value.vatId!,
      phone: this.fg.value.phone!,
      address: this.fg.value.address!,
      postalCode: this.fg.value.postalCode!,
      city: this.fg.value.city!,
      state: this.fg.value.city!,
      correspondencePersonId: this.fg.value.correspondencePersonId!,
    };

    this.companyService.updateCompany(updateModel).subscribe((_) => {
      console.log(updateModel)
      console.log('Company successfully updated');
      this.router.navigate(['../..'], { relativeTo: this.activatedRoute });
    });
  }
} 