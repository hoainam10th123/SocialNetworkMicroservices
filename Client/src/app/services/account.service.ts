import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client-ts';
import { ReplaySubject } from 'rxjs';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  private manager = new UserManager(this.getClientSettings());
  private user: User | null;
  
  constructor(private presence: PresenceService) {
    // neu de dong nay se loi user
    // this.manager.getUser().then(user => {
    //   this.user = user;      
    // });
  }

  setCurrentUser(user: User){
    if(user){  
      this.user = user    
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user); 
    }
  }

  get CurrentUser(){
    return this.user
  }

  get currentUsername(): string{
    console.log(this.user.access_token)
    return this.user.access_token
  }

  login() {
    return this.manager.signinRedirect();
  }

  async signout() {
    localStorage.removeItem('user');    
    this.currentUserSource.next(null);
    this.presence.stopHubConnection()
    await this.manager.signoutRedirect();    
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  // get authorizationHeaderValue(): string {
  //   if (this.user) {
  //     return `${this.user.token_type} ${this.user.access_token}`;
  //   }
  //   return null;
  // }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this.setCurrentUser(this.user)
    this.presence.createHubConnection(this.user)
  }

  getClientSettings(): UserManagerSettings {
    return {
      authority: 'http://localhost:5077',// url identity server
      client_id: 'angularClient',
      redirect_uri: 'http://localhost:4200/auth-callback',
      post_logout_redirect_uri: 'http://localhost:4200',
      response_type: 'code',
      scope: 'openid profile address email roles userAPI postAPI notificationAPI chatAPI',
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
    };
  }

  getUsername(): string{
    return this.getDecodedToken().sub
  }

  token(){
    console.log(this.user.access_token)
  }

  private getDecodedToken() {
    return JSON.parse(atob(this.user.access_token.split('.')[1]));
  }
}
