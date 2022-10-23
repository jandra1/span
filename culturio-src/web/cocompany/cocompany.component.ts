import { Component, OnInit } from '@angular/core';
import { map, Observable, shareReplay } from 'rxjs';
import { cocompanyDto } from './models/cocompanyDto';
import { GetCocompanyDto } from './models/getcocompanyDto';
import { CocompanyService } from './services/cocompany.service';

@Component({
  selector: 'app-cocompany',
  templateUrl: './cocompany.component.html',
  styleUrls: ['./cocompany.component.scss']
})
export class CocompanyComponent implements OnInit {
  CocObjects$: Observable<cocompanyDto[]>;
  CocObjectById$:Observable<cocompanyDto>;
  totalCount$:Observable<number>;

  searchValue = '';
  visible = false;
  sortOrder:string

  constructor(private CocObjectService: CocompanyService) { }

  ngOnInit(): void {
    this.getCocObjects();
  }
  getCocObjects(){
    const model:GetCocompanyDto={
      sortOrder:'asc',
      searchValue: '',
      page: 1,
      pageSize: 10,
  };
  const source = this.CocObjectService.getCocompany(model).pipe(shareReplay());
  this.totalCount$ = source.pipe(map((x) => x.totalCount));
  this.CocObjects$ = source.pipe(map((x) => x.cultureObjectCompanies));

  }
  getCocompanyById(id: number) {
    this.CocObjectById$ = this.CocObjectService.getCocompanyById(id);
  }

 search() {}

}
