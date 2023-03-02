import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NgxSpinnerService } from 'ngx-spinner';
import { IPost } from '../models/post';
import { PostParams } from '../models/postParams';
import { AccountService } from '../services/account.service';
import { CommentService } from '../services/comment.service';
import { PostService } from '../services/post.service';


@Component({
  selector: 'app-newfeed',
  templateUrl: './newfeed.component.html',
  styleUrls: ['./newfeed.component.css']
})
export class NewfeedComponent implements OnInit{
  faEdit = faEdit
  faTrash = faTrash
  postParams = new PostParams()
  posts: IPost[] = [];
  totalPages: number;
  totalCount: number;
  noiDung: string = ''

  constructor(private postService: PostService, 
    private commentService: CommentService, 
    private spinner: NgxSpinnerService, public accountService: AccountService){}
  
  ngOnInit(): void {
    this.loadPosts()
  }

  loadPosts(){
    this.spinner.show();
    setTimeout(() =>{
      this.postService.getPosts(this.postParams).subscribe(res=>{
        this.posts = res.data
        this.totalCount = res.count
        this.totalPages = Math.ceil(res.count / this.postParams.pageSize);  
        this.spinner.hide(); 
      })
    }, 2000)    
  }

  addPost(){
    this.postService.addPost('Tieu de', 'Noi dung').subscribe(res =>{
      console.log(res)
    })
  }

  deletePost(id: string){
    this.postService.deletePost(id).subscribe(()=> console.log('xoa thanh cong'))
  }

  onSubmit(form: NgForm, postId: string, userNameNhan: string): void {
    console.log(form);
    if(this.noiDung !== ''){
      this.commentService.addComment(this.noiDung, postId, userNameNhan).subscribe(res=>{
        let post = this.posts.find(x=>x.id === res.postId)
        if(post){
          post.comments.push(res)
        }
        this.noiDung = ''
      })
    }    
  }

  deleteComment(id: string){
    this.commentService.deleteComment(id).subscribe(res =>{
      console.log(res)
    })
  }
  
}
