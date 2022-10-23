import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '../users/users.component';


const routes: Routes = [
  { path: '', component: UsersComponent },
  { path: 'user', component: UsersComponent  },   //pocetna stranica usera - UserComponent
  { path: 'couser', /*component:*/  },   //pocetna stranicaco usera - CoUserComponent
  { path: 'padmin', /*component:*/  },   //pocetna stranica usera - UserComponent
  { path: 'copanyowner', /*component:*/  },   //pocetna stranica usera - UserComponent
  { path: 'copanyadmin', /*component:*/  },   //pocetna stranica usera - UserComponent
  { path: 'cocowner', /*component:*/  },   //pocetna stranica usera - UserComponent
  { path: 'coadmin', /*component:*/  },   //pocetna stranica usera - UserComponent




];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BooksRoutingModule {}