import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import {NzFormModule} from "ng-zorro-antd/form";
import { CobjectService } from './services/cobject.service';
import { CobjectRoutingModule } from './cobject-routing.module';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzSelectModule } from 'ng-zorro-antd/select';


@NgModule({
  declarations: [CobjectRoutingModule.delcaredComponents],
  imports: [
    CommonModule,
    CobjectRoutingModule,
    HttpClientModule,
    FormsModule,
    NzInputModule,
    NzTableModule,
    NzLayoutModule,
    NzFormModule,
    NzDropDownModule,
    ReactiveFormsModule,
    NzButtonModule,
    NzIconModule,
    NzDividerModule,
    NzIconModule,
    NzSelectModule
  ],
  providers:[CobjectService]
})
export class CobjectModule { }