import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzFormModule } from 'ng-zorro-antd/form';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { CocompanyRoutingModule } from './cocompany-routing.model';
import { CompanyService } from '../company/services/company.service';
import { UpdateCocompanyComponent } from './update-cocompany/update-cocompany.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzIconModule } from 'ng-zorro-antd/icon';


@NgModule({
  declarations: [CocompanyRoutingModule.routingComponents],
  imports: [
    CommonModule,
    NzInputModule,
    NzTableModule,
    NzLayoutModule,
    NzDropDownModule,
    NzFormModule,
    ReactiveFormsModule,
    CocompanyRoutingModule,
    NzButtonModule,
    NzDividerModule,
    NzIconModule,
    CocompanyRoutingModule,
    FormsModule,
    NzButtonModule,
    NzIconModule
    
  ],
  providers: [CompanyService],
})
export class CocompanyModule {}