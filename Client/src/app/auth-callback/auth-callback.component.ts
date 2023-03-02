import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { PresenceService } from '../services/presence.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css']
})
export class AuthCallbackComponent implements OnInit{

  constructor(private account: AccountService, 
    private router: Router, 
    private presence: PresenceService){

  }

  async ngOnInit() {
    await this.account.completeAuthentication()
    this.presence.createHubConnection(this.account.CurrentUser)
    this.router.navigateByUrl('/neewfeed')
  }

}
