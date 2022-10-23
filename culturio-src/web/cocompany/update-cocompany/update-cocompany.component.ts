import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CompanyService } from 'src/app/company/services/company.service';
import { cocompanyDto } from '../models/cocompanyDto';
import { UpdateCocompanyModel } from '../models/update-cocompany.model';
import { CocompanyService } from '../services/cocompany.service';

@Component({
  selector: 'app-update-cocompany',
  templateUrl: './update-cocompany.component.html',
  styleUrls: ['./update-cocompany.component.scss']
})
export class UpdateCocompanyComponent implements OnInit {

  id:number;
  
  constructor(
    private fb: FormBuilder,
    private cocompanyService: CocompanyService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
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
    companyLogo:  [''],
    iBAN: [''],
  });

  ngOnInit(): void {
    this.id=this.activatedRoute.snapshot.params['id'];
    this.cocompanyService.getCocompanyById(this.id).subscribe(data=>{
      this.fillForm(data);
    })
  }

  private fillForm(data:cocompanyDto){
    this.fg.setValue({
      id:data.id,
      name: data.name,
      taxId: data.taxId,
      vatId: data.vatId,
      phone: data.phone,
      address: data.address,
      postalCode: data.postalCode,
      city: data.city,
      state: data.state,
      correspondencePersonId: null,
      companyLogo: data.companyLogo,
      iBAN: null,
    })
  }

  onSubmit(): void{

    const updateModel: UpdateCocompanyModel = {
      id:this.id!,
      
      name: this.fg.value.name!,
      taxId: this.fg.value.taxId!,
      vatId: this.fg.value.vatId!,
      phone: this.fg.value.phone!,
      address: this.fg.value.address!,
      postalCode: this.fg.value.postalCode!,
      city: this.fg.value.city!,
      state: this.fg.value.state!,
      correspondencePersonId: this.fg.value.correspondencePersonId!,
      companyLogo: this.fg.value.companyLogo!,
      iBAN: this.fg.value.iBAN!,

    };
    console.log(updateModel);
    this.cocompanyService.updateCocompany(updateModel).subscribe((_) => {
      console.log(updateModel)
      console.log('Culture object company successfully updated');
      this.router.navigate(['../..'], { relativeTo: this.activatedRoute });
    });
  }

}
