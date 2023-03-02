import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { apiPostUrl } from '../enviroments/env';
import { IPagination, Pagination } from '../models/pagination';
import { IPost } from '../models/post';
import { PostParams } from '../models/postParams';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  pagination = new Pagination<IPost>();
  
  constructor(private http: HttpClient) { }

  getPosts(postParams: PostParams){    
    let params = new HttpParams();
    params = params.append('pageNumber', postParams.pageNumber.toString());
    params = params.append('pageSize', postParams.pageSize.toString());    
      
    return this.http.get<IPagination<IPost>>(apiPostUrl + 'posts', { observe: 'response', params }).pipe(
      map(response => {
        this.pagination = response.body;        
        return this.pagination;
      })
    );
  }

  addPost(title: string, noiDung: string){
    return this.http.post<IPost>(apiPostUrl + 'posts', {title, noiDung})
  }

  deletePost(id: string){
    return this.http.delete(apiPostUrl + 'posts/'+id)
  }
}
