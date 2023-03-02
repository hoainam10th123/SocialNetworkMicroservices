import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiUserUrl } from '../enviroments/env';
import { UsersNearestStore } from '../models/userLocation';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  findNearestUser(long: number, lat: number){
    return this.http.get<UsersNearestStore>(apiUserUrl + `user?lng=${long}&lat=${lat}`)
  }
}
