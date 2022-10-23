import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzFormModule } from 'ng-zorro-antd/form';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { CompanyService } from './services/company.service';
import { NzInputModule } from 'ng-zorro-antd/input';
import { CompanyRoutingModule } from '../company/company-routing.module';
import { UpdateCompanyComponent } from './update-company/update-company.component';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';




@NgModule({
  declarations: [CompanyRoutingModule.routingComponents],
  imports: [
    CompanyRoutingModule,
    CommonModule,
    NzInputModule,
    NzTableModule,
    NzLayoutModule,
    NzDropDownModule,
    NzFormModule,
    ReactiveFormsModule,
    NzGridModule,
    NzDividerModule,
    NzButtonModule,
    NzIconModule,
    FormsModule,
    NzButtonModule,
    NzIconModule
  ],
  providers: [CompanyService],
})
export class CompanyModule {}