import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { UserService } from './services/user.service';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzSelectModule } from 'ng-zorro-antd/select';

import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { NzOptionComponent, NzOptionItemComponent } from 'ng-zorro-antd/select';
import { StatsComponent } from './stats/stats.component';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import * as ApexCharts from 'apexcharts';
@NgModule({
  declarations: [UsersRoutingModule.delcaredComponents, StatsComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,
    NzInputModule,
    NzTableModule,
    NzLayoutModule,
    NzDropDownModule,
    FormsModule,
    NzFormModule,
    ReactiveFormsModule,
    NzButtonModule,
    NzDividerModule,
    NzIconModule,
    NzSelectModule,
    NzStatisticModule,
  ],
  providers: [UserService],
})
export class UsersModule {}
