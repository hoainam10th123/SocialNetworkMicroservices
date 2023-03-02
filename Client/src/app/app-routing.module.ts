import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { SiteLayoutComponent } from './layout/site-layout/site-layout.component';
import { NewfeedComponent } from './newfeed/newfeed.component';
//import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { 
    path: '', 
    component: SiteLayoutComponent,
    children: [
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'profile', component: ProfileComponent },
      //{ path: 'login', component: LoginComponent },
      { path: 'auth-callback', component: AuthCallbackComponent },
      { path: 'neewfeed', component:  NewfeedComponent},
      { path: 'server-error', component: ServerErrorComponent }
    ]
  },

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
