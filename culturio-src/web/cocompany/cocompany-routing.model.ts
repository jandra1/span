import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CocompanyComponent } from './cocompany.component';
import { CreateCocompanyComponent } from './create-cocompany/create-cocompany.component';
import { UpdateCocompanyComponent } from './update-cocompany/update-cocompany.component';


const routes: Routes = 
[{ path: '', component: CocompanyComponent },
{ path: 'create', component: CreateCocompanyComponent },
{ path: 'edit/:id', component: UpdateCocompanyComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class CocompanyRoutingModule {
      public static routingComponents = [CocompanyComponent, CreateCocompanyComponent, UpdateCocompanyComponent];
  }




