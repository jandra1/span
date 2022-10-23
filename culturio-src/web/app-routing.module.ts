import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { PAdminUsersComponent } from './p-admin/p-admin-users/p-admin-users.component';
import { ProfileComponent } from './profile/profile.component';
import { QrcodeComponent } from './qrcode/qrcode.component';
import { UsersComponent } from './users/users.component';
import { MsalGuard } from '@azure/msal-angular';
import { CouserComponent } from './couser/couser.component';
import { VisitComponent } from './visit/visit.component';
import { StatsComponent } from './users/stats/stats.component';

const routes: Routes = [
  {
    // Needed for hash routing
    path: 'error',
    component: WelcomeComponent,
  },
  {
    // Needed for hash routing
    path: 'state',
    component: WelcomeComponent,
  },
  {
    // Needed for hash routing
    path: 'code',
    component: WelcomeComponent,
  },
  {
    path: '',
    component: WelcomeComponent,
  },
  {
    path: 'user-view',
    component: UsersComponent,
    canActivate: [MsalGuard],
  },

  //{ path: 'padmin/users', component: PAdminUsersComponent },
  { path: '', pathMatch: 'full', redirectTo: '/welcome' },
  {
    path: 'welcome',
    loadChildren: () =>
      import('./pages/welcome/welcome.module').then((m) => m.WelcomeModule),
  },
  {
    path: 'users',
    loadChildren: () =>
      import('./users/users.module').then((m) => m.UsersModule),
  },
  {
    path: 'company',
    loadChildren: () =>
      import('./company/company.module').then((m) => m.CompanyModule),
  },
  {
    path: 'cocompany',
    loadChildren: () =>
      import('./cocompany/cocompany.module').then((m) => m.CocompanyModule),
  },
  { path: 'login', component: LoginComponent },
  {
    path: 'cobject',
    loadChildren: () =>
      import('./cobject/cobject.module').then((m) => m.CobjectModule),
  },
  {
    path: 'visit',
    loadChildren: () =>
      import('./visit/visit.module').then((m) => m.VisitModule),
  },
  {
    path: 'qrcode',
    loadChildren: () =>
      import('./qrcode/qrcode.module').then((m) => m.QrcodeModule),
  },
  { path: 'padmin/users', component: PAdminUsersComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'couser', component: CouserComponent },
  { path: 'stats', component: StatsComponent },
];

const isIframe = window !== window.parent && !window.opener;

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: true,
      // Don't perform initial navigation in iframes

      //initialNavigation = 'enabled'
      initialNavigation: !isIframe ? 'enabledNonBlocking' : 'disabled',
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
