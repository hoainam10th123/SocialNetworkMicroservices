import { AfterViewChecked, AfterViewInit, ChangeDetectionStrategy, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { MessageService } from 'src/app/services/message.service';
import { faClose, faMinus } from '@fortawesome/free-solid-svg-icons';

@Component({
  //changeDetection: ChangeDetectionStrategy.OnPush,//fix error #scrollMe [scrollTop]="scrollMe.scrollHeight"
  selector: 'app-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.css'],
  providers: [MessageService]//separate services independently for every component
})
export class ChatBoxComponent implements OnInit, AfterViewInit, AfterViewChecked, OnDestroy{
  @Input() username: string;//information of chat box; username nhan message
  messageContent: string;
  @Input() right: number;
  @Output() removeChatBox = new EventEmitter();
  //@Output() miniChatBox = new EventEmitter();
  @ViewChild('messageForm') messageForm: NgForm;

  @ViewChild('scrollMe', {static: true}) myScrollContainer: ElementRef;
  currentUser: string;
  totalPage: number;
  pageNumber = 1;
  faClose = faClose
  faMinus = faMinus

  constructor(public accountService: AccountService, 
    public messageService: MessageService){
    this.currentUser = accountService.getUsername();
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

  ngOnInit(): void {
    this.messageService.createHubConnection(this.accountService.CurrentUser, this.username);
  }
  
  ngAfterViewChecked() {
    this.scrollToBottom()        
  }

  scrollToBottom() {     
    this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    return this.myScrollContainer.nativeElement.scrollTop !== 0;
  }

  ngAfterViewInit() {
    var chatBox = document.getElementById(this.username);
    chatBox.style.right = this.right + "px";
  }

  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm.reset();
      this.scrollToBottom();
    })
  }

  closeBoxChat() {
      this.removeChatBox.emit(this.username);
  }

  // minimumBoxChat() {
  //   this.closeBoxChat();
  //   this.miniChatBox.emit(this.username);
  // }

  loading: boolean;
  onScrollUp() {
    if (this.pageNumber < this.totalPage) {
      this.loading = true;
      this.pageNumber++;
      this.messageService.getMessageThread(this.pageNumber, 10, this.username).subscribe(res => {
        this.loading = false;
      })
    }
  }
}
