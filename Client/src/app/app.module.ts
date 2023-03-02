import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {ToastrModule} from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";

import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { JwtInterceptor } from './interceptor/jwt.interceptor';
import { NewfeedComponent } from './newfeed/newfeed.component';
import { ErrorInterceptor } from './interceptor/error.interceptor';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { ChatBoxComponent } from './components/chat-box/chat-box.component';
import { FriendListComponent } from './components/friend-list/friend-list.component';


@NgModule({
  declarations: [
    AppComponent,
    AuthCallbackComponent,
    NewfeedComponent,
    ServerErrorComponent,
    ChatBoxComponent,
    FriendListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-left',
      preventDuplicates: true
    })
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
