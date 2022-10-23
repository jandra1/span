import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './company.component';
import { CreateCompanyComponent } from './create-company/create-company.component';
import { UpdateCompanyComponent } from './update-company/update-company.component';

const routes: Routes = 
[{ path: '', component: CompanyComponent },
{ path: 'create', component: CreateCompanyComponent },
{ path: 'edit/:id', component: UpdateCompanyComponent },


];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class CompanyRoutingModule {
      public static routingComponents = [CompanyComponent, CreateCompanyComponent, UpdateCompanyComponent];

  }




