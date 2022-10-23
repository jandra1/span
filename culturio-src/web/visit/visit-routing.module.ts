import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CinemaComponent } from './cinema/cinema.component';
import { MuseumComponent } from './museum/museum.component';
import { TheatreComponent } from './theatre/theatre.components';
import { VisitComponent } from './visit.component';


const routes: Routes = [
    { path: '', component: VisitComponent },
    { path: 'cinema', component: CinemaComponent },
    { path: 'museum', component: MuseumComponent },
    { path: 'theatre', component: TheatreComponent },



];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class VisitRoutingModule {
    public static delcaredComponents = [
      VisitComponent,
      CinemaComponent,
      MuseumComponent,
      TheatreComponent
    ]
  }