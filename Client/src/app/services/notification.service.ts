import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiNotiUrl } from '../enviroments/env';
import { INotification } from '../models/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private http: HttpClient) { }

  getNotifications(){
    return this.http.get<INotification[]>(apiNotiUrl + 'Notification')
  }
}
