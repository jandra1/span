import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './create-user/create-user.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { UsersComponent } from './users.component';


const routes: Routes = [
  { path: '', component: UsersComponent },
  { path: 'create', component: CreateUserComponent },
  { path:'edit/:id', component:UpdateUserComponent},


  //{ path: 'cobjects', component: CobjectComponent },  //kulturni objekti
  /*{ path: 'theatre', component:  }, */  //kazaliste
  /*{ path: 'museum', component:  },*/  //muzej


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {
  public static delcaredComponents = [
    UsersComponent,
    CreateUserComponent,
    UpdateUserComponent
  ]
}
