import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from '../users/create-user/create-user.component';


const routes: Routes = [
  { path: '', },
  //{ path: 'createuser', component: CreateUserComponent  },   //stvaranje novog usera - UserComponent


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BooksRoutingModule {}