import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StreamService {

  private senderUserSource = new Subject<string>();
  senderUsername$ = this.senderUserSource.asObservable();

  private onClickUserLocationSource = new Subject<string>();
  onClickUserLocation$ = this.onClickUserLocationSource.asObservable();

  constructor() { }

  set SenderUsername(value: string) {
    this.senderUserSource.next(value);
  }

  set UserLocation(value: string) {
    this.onClickUserLocationSource.next(value);
  }
}
