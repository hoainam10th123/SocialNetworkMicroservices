import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client-ts';
import { AccountService } from './services/account.service';
import { PresenceService } from './services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'NgxSocialNetwork';

  constructor(private accountService: AccountService,
    private presence: PresenceService){}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    console.log(user)
    if (user) {
      this.accountService.setCurrentUser(user);
      this.presence.createHubConnection(user);    
    }
  }
}
