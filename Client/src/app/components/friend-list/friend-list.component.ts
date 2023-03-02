import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserChatBox } from 'src/app/models/userChatBox';
import { PresenceService } from 'src/app/services/presence.service';
import { StreamService } from 'src/app/services/stream.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent {
  chatBoxUsers: UserChatBox[] = [];

  constructor(public presence: PresenceService, private stream: StreamService, private toastr: ToastrService){
    this.stream.senderUsername$.subscribe(senderUsername =>{
      if(this.chatBoxUsers.length < 1){
        this.selectUser(senderUsername)
      }else{
        this.toastr.info(`You have a message from ${senderUsername}`, 'Information')
      }
    })

    this.stream.onClickUserLocation$.subscribe(username=>{
      this.selectUser(username)
    })
  }

  selectUser(user: string) {
    switch (this.chatBoxUsers.length % 2) {
      case 0: {
        let u = this.chatBoxUsers.find(x => x.user === user);
        if (u) {
          this.chatBoxUsers = this.chatBoxUsers.filter(x => x.user !== user);
          this.chatBoxUsers.push(u);
        } else {
          this.chatBoxUsers.push(new UserChatBox(user, 250));
        }
        break;
      }
      case 1: {
        let u = this.chatBoxUsers.find(x => x.user === user);
        if (u) {
          this.chatBoxUsers = this.chatBoxUsers.filter(x => x.user !== user);
          this.chatBoxUsers.push(u);
        } else {
          this.chatBoxUsers.push(new UserChatBox(user, 250 + 325));
        }
        break;
      }      
    }
  }

  removeChatBox(event: string) {
    this.chatBoxUsers = this.chatBoxUsers.filter(x => x.user !== event);
  }
}
