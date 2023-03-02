import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { User } from 'oidc-client-ts';
import { BehaviorSubject, take } from 'rxjs';
import { hubUrl } from '../enviroments/env';
import { StreamService } from './stream.service';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {

  private hubConnection: HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();
  
  constructor(private stream: StreamService, private toastr: ToastrService){ }

  createHubConnection(user: User) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl + 'presence', {
        accessTokenFactory: () => user.access_token
      })
      .withAutomaticReconnect()
      .build()

    this.hubConnection
      .start()
      .catch(error => console.log(error));

    this.hubConnection.on('UserIsOnline', (username: string) => {
      this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
        // fix bug trung 2 username, neu user chua co trong array thi moi them vao mang
        const isExist = usernames.some(x=>x === username)
        if(!isExist){
          this.onlineUsersSource.next([...usernames, username])
        }        
      })
      //this.toastr.info(username.displayName+ ' has connect')
    })

    this.hubConnection.on('UserIsOffline', (username: string) => {
      this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
        this.onlineUsersSource.next([...usernames.filter(x => x !== username)])
      })
      //this.toastr.warning(username.displayName + ' disconnect')
    })

    this.hubConnection.on('GetOnlineUsers', (usernames: string[]) => {
      this.onlineUsersSource.next(usernames);
    })

    this.hubConnection.on('NewMessageReceived', (sender: string) => {
      // lang nghe stream nay tai friendList component, de show chat box
      this.stream.SenderUsername = sender;
    })

    this.hubConnection.on('NewNotificationReceived', (sender: string) => {
      this.toastr.info(sender+ ' comment into your post')
    })
  }

  stopHubConnection() {
    this.hubConnection.stop().catch(error => console.log(error));
  }
}
