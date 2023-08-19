import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ICommentary } from '../../models/ICommentary';
import { CommentParams } from '../../models/commentParams';
import { CommentariesService } from './commentaries.service';
import { IPagination } from '../../models/Pagination';
import { IUser } from '../../models/IUser';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-commentaries',
  templateUrl: './commentaries.component.html',
  styleUrls: ['./commentaries.component.scss']
})
export class CommentariesComponent implements OnInit, AfterViewInit {

  @Input() postId?: number;
  userId?: string;
  public comments?: ICommentary[];

  commentParams = new CommentParams();
  totalCount = 0;
  currentUser$?: Observable<IUser | null>;
  commentForm: FormGroup = new FormGroup({
    id: new FormControl(),
    message: new FormControl(),
    appUserId: new FormControl(),
    postId: new FormControl(),
  })

  constructor(private _commentService: CommentariesService
    , private accountService: AccountService) {

  }
  ngAfterViewInit(): void {
    this.commentForm.get('postId')?.setValue(this.postId);
    console.log(this.currentUser$);

    this.currentUser$?.subscribe(user => {
      console.log(user);
      if (user) {
        this.userId = user.id;
        this.commentForm.get('appUserId')?.setValue(user.id);
      }
    })
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    if (this.postId)
      this.GetCommentary(this.postId);
  }

  GetCommentary(postId: number) {
    this.commentParams.postId = postId;
    this._commentService.GetCommentaries(this.commentParams).subscribe(
      response => {
        console.log("commentaries", response)
        if (response) {
          this.comments = response.data as ICommentary[];
          this.commentParams.PageIndex = response.pageIndex;
          this.commentParams.pageSize = response.pageSize;
          this.totalCount = response.count;
        }
      });
  }

  onSubmit() {
    var comment = Object.assign({}, this.commentForm.value);
    comment.appUserId = this.userId;
    comment.id = comment.id ? comment.id : 0;
    console.log(comment);
    if (comment)
      this._commentService.UpdateCommentary(comment).subscribe(respose => {
        console.log(respose);
        this.clearForm();
        if (this.postId)
          this.GetCommentary(this.postId);
      });
  }

  clearForm() {
    this.commentForm.get("message")?.setValue(null);
    this.commentForm.get("id")?.setValue(0);
  }
  editClicked(comment: ICommentary) {
    console.log(comment);
    this.commentForm.patchValue(comment);
  }

  deleteClicked(id: number) {
    this._commentService.DeleteCommentary(id).subscribe(response => {
      console.log(response);
      if (this.postId)
        this.GetCommentary(this.postId);
    })
  }

  onPageChanged(event: any) {
    this.commentParams.PageIndex = event.page;
    if (this.postId)
      this.GetCommentary(this.postId);
  }

  public handleMissingImage(event: Event) {
    console.log("see error");
    (event.target as HTMLImageElement).style.display = 'none';
  }
}
