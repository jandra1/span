
import { AfterViewInit, ElementRef, ViewChild } from '@angular/core';

import {
  debounceTime,
  distinctUntilChanged,
  forkJoin,
  fromEvent,
  map,
  Observable,
  shareReplay,
} from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { NzFormModule } from 'ng-zorro-antd/form'

import {
  NzPaginationComponent,
  NzPaginationModule,
} from 'ng-zorro-antd/pagination';
import { HttpClient } from '@angular/common/http';
import { CompanyDto } from './models/companyDto';
import { CompanyService } from './services/company.service';
import { GetCompanyDto } from './models/getcopmanyDto';



@Component({
  selector: 'app-company', 
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss'],

})
export class CompanyComponent implements OnInit {
  companyObjects$: Observable<CompanyDto[]> | undefined;
  companyById$: Observable<CompanyDto>;
  totalCount$:Observable<number>;

  searchValue = '';
  visible = false;
  sortOrder:string

  constructor(private companyService: CompanyService) { }

  ngOnInit(): void {
    this.getCompanys();
  }

  getCompanys(){
    const model:GetCompanyDto={
      sortOrder:'asc',
      searchValue: '',
      page: 1,
      pageSize: 10,
  };
  const source = this.companyService.getCompanys(model).pipe(shareReplay());
  this.totalCount$ = source.pipe(map((x) => x.totalCount));
  this.companyObjects$ = source.pipe(map((x) => x.companies));
}

  getCompanyById(id: number) {
    this.companyById$ = this.companyService.getCompanyById(id);
  }

  public search() {}

}
