import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VisitRoutingModule } from './visit-routing.module';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { VisitService } from './services/visit.service';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';


@NgModule({
    declarations: [VisitRoutingModule.delcaredComponents],
    imports: [
      CommonModule,
      VisitRoutingModule,
      NzButtonModule,
      NzDividerModule,
      NzIconModule,
      NzLayoutModule,
      NzInputModule,
      NzTableModule,
      NzFormModule,
      NzDropDownModule,
      NzIconModule,
      NzCarouselModule,
      
    ],
    providers: [VisitService],
  })
  export class VisitModule {}