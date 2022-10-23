import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CobjectComponent } from './cobject.component';
import { UpdateCobjectComponent } from './update-cobject/update-cobject.component';
import { CreateCobjectComponent } from './create-cobject/create-cobject.component';


const routes: Routes = [
  { path: '', component: CobjectComponent },
  { path: 'create', component: CreateCobjectComponent },
  {path:'edit/:id',component:UpdateCobjectComponent},

  /*{ path: 'cinema', component:  },*/   //kino
  /*{ path: 'theatre', component:  }, */  //kazaliste
  /*{ path: 'museum', component:  },*/  //muzej


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CobjectRoutingModule {
  public static delcaredComponents = [
    CobjectComponent,
    CreateCobjectComponent,
    UpdateCobjectComponent
  ]
}