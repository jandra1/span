import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { waitForAsync } from '@angular/core/testing';
import { ConsoleSqlOutline } from '@ant-design/icons-angular/icons';
import { Observable } from 'rxjs';
import { CobjectDto } from 'src/app/cobject/models/cobjectDto';
import { CobjectService } from 'src/app/cobject/services/cobject.service';
import { GetVisitsDto } from 'src/app/visits/models/getVisitsDto';
import { VisitDto } from 'src/app/visits/models/visitDto';
import { VisitsGridInfoDto } from 'src/app/visits/models/visitsGridInfoDto';
import { VisitService } from 'src/app/visits/services/visits.service';
import { Chart } from 'chart.js';
@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss'],
})
export class StatsComponent implements OnInit {
  totalVisits = 0;
  cinemasVisited = 0;
  museumsVisited = 0;
  theatersVisited = 0;
  cObjectArr: CobjectDto[] = [];
  visits$: Observable<VisitsGridInfoDto>;
  visits: VisitDto[];
  temp: CobjectDto[];
  showTable = 0;
  visitCounter: number;

  constructor(
    private visitService: VisitService,
    private cobjectService: CobjectService
  ) {}

  ngOnInit(): void {
    this.getVisits();
    this.visits$.subscribe((result) => {
      this.totalVisits = result.totalCount;
      this.visitCounter = this.totalVisits;
      this.visitCounter < 10 ? this.visitCounter : 10;
      this.visits = result.visits;
      this.fillCultureObjectsArr();
      this.countTypesOfObjects();
    });
  }

  getVisits() {
    const model: GetVisitsDto = {
      sortOrder: 'asc',
      searchValue: '',
      page: 1,
      pageSize: 10,
    };
    this.visits$ = this.visitService.getVisitOfUser(model);
  }

  fillCultureObjectsArr() {
    let arr: CobjectDto[] = [];
    for (let i = 0; i < this.totalVisits; i++) {
      this.cobjectService
        .getCobjectById(this.visits[i].cultureObjectId)
        .subscribe((output) => {
          arr.push(output);
          if (output.cultureObjectType == 'Cinema') {
            this.cinemasVisited++;
            console.log(this.cinemasVisited);
          } else if (output.cultureObjectType == 'Museum') {
            this.museumsVisited++;
          } else if (output.cultureObjectType == 'Theater') {
            this.theatersVisited++;
          }
          this.showTable++;
        });
    }
    this.countTypesOfObjects();
    this.cObjectArr = arr;
  }

  countTypesOfObjects() {
    this.cObjectArr.forEach((element) => {
      console.log('\n \n \n', element);
      if (element.cultureObjectType == 'Cinema') {
        this.cinemasVisited = this.cinemasVisited + 1;
      } else if (element.cultureObjectType == 'Museum') {
        this.museumsVisited++;
      } else if (element.cultureObjectType == 'Theater') {
        this.theatersVisited++;
      }
    });
  }
}
