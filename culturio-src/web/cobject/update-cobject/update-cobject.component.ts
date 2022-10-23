import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { zh_CN } from 'ng-zorro-antd/i18n';
import { forkJoin, shareReplay,map, Observable, first } from 'rxjs';
import { CobjectDto } from '../models/cobjectDto';
import { UpdateCobjectModel } from '../models/update-cobject.model';
import { CobjectService } from '../services/cultureObject.service';


@Component({
  selector: 'app-update-cobject',
  templateUrl: './update-cobject.component.html',
  styleUrls: ['./update-cobject.component.scss']
})
export class UpdateCobjectComponent implements OnInit {

  id:number;
  
  constructor(
    private fb: FormBuilder,
    private cobjectService:CobjectService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {};

  fg = this.fb.group({
    id:new FormControl<number | null>(null),
    name: ['', Validators.required],
    phone: ['', Validators.required],
    address: ['', Validators.required],
    postalCode: new FormControl<number | null>(null),
    city: ['', Validators.required],
    state: ['', Validators.required],
    workingHours: ['', Validators.required],
    cultureObjectTypeId: new FormControl<number | null>(null),
    notes:['', Validators.required],
    responsiblePersonId:new FormControl<number | null>(null),
    cultureObjectCompanyId:new FormControl<number | null>(null)
  });

  ngOnInit(): void {
    this.id=this.activatedRoute.snapshot.params['id'];
    this.cobjectService.getCultureObjectById(this.id).subscribe(data=>{
      this.fillForm(data);
    })
    

  }

  private fillForm(data:CobjectDto){
    this.fg.setValue({
      id:data.id,
      name:data.name,
      phone:data.phone,
      address:data.address,
      postalCode:+data.postalCode,
      city:data.city,
      state:data.state,
      workingHours:data.workingHours,
      cultureObjectTypeId: null,
      notes:data.notes,
      responsiblePersonId:null,
      cultureObjectCompanyId:null
    })
  }

  onSubmit(): void{

    const updateModel: UpdateCobjectModel = {
      id:this.id!,
      name: this.fg.value.name!,
      phone: this.fg.value.phone!,
      address: this.fg.value.address!,
      postalCode: this.fg.value.postalCode!,
      city: this.fg.value.city!,
      state: this.fg.value.state!,
      workingHours: this.fg.value.workingHours!,
      cultureObjectTypeId: this.fg.value.cultureObjectTypeId!,
      notes: this.fg.value.notes!,
      responsiblePersonId: this.fg.value.responsiblePersonId!,
      cultureObjectCompanyId: this.fg.value.cultureObjectCompanyId!
    };

    this.cobjectService.updateCobject(updateModel).subscribe((_) => {
      console.log(updateModel)
      console.log('Culture object successfully updated');
      this.router.navigate(['../..'], { relativeTo: this.activatedRoute });
    });
  }
} 