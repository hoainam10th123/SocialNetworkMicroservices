import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiPostUrl } from '../enviroments/env';
import { IComment } from '../models/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http: HttpClient) { }

  addComment(noiDung: string, postId: string, userNameNhan: string){
    return this.http.post<IComment>(apiPostUrl + 'comments', {noiDung, postId, userNameNhan})
  }

  deleteComment(id: string){
    return this.http.delete<any>(apiPostUrl + 'comments/'+id)
  }
}
