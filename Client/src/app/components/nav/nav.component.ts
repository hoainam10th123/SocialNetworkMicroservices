import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faBell, faUserFriends, faCircle } from '@fortawesome/free-solid-svg-icons';
import { INotification } from 'src/app/models/notification';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { UserLocation } from 'src/app/models/userLocation';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxSpinnerService } from "ngx-spinner";
import { StreamService } from 'src/app/services/stream.service';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    CommonModule, 
    RouterModule, 
    BsDropdownModule, 
    FontAwesomeModule,
    NgxSpinnerModule
  ],
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  faBell = faBell
  faUserFriends = faUserFriends
  faCircle = faCircle
  notifications: INotification[] = []
  friends: UserLocation[] = []

  constructor(public account: AccountService, 
    private notiService: NotificationService,
    private userService: UserService, 
    private spinner: NgxSpinnerService, 
    private stream: StreamService){}

  ngOnInit(): void {
    
  }

  async login(){
    await this.account.login()
    //sau khi login xong se redict toi AuthCallbackComponent. luc do navigateto neewfeed    
  }

  async logout(){
    await this.account.signout()    
  }

  onClickNoti(){
    this.spinner.show();
    setTimeout(() =>{
      this.notiService.getNotifications().subscribe(res=> {
        this.notifications = res
        this.spinner.hide();
      })
    }, 2000)
    
  }

  findNearestFriends(){    
    this.spinner.show();
    setTimeout(()=>{
      this.userService.findNearestUser(79.821602, 28.626137).subscribe(res=>{
        this.friends = res.resultUsers
        console.log(this.friends)
        this.spinner.hide();
      })
    }, 2000)    
  }

  showChatBox(username: string){
    // show chat box tai friend list
    this.stream.UserLocation = username
  }
}
