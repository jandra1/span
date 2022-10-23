import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QrcodeComponent } from './qrcode.component';



const routes: Routes = [
    { path: '', component: QrcodeComponent },
 
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })
  export class QrcodeRoutingModule {
    public static delcaredComponents = [
      QrcodeComponent
    ]
  }