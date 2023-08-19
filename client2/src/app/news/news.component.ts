import { Component, OnInit } from '@angular/core';
import { IPost } from '../shared/models/IPost';
import { NewsService } from './news.service';
import { NewsParams } from '../shared/models/newsParams';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { IUser } from '../shared/models/IUser';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
  public searchString = "";
  public isAdmin = false;
  public descriptionLength = 100;

  currentUser$?: Observable<IUser | null>;

  searchForm = new FormGroup({
    search: new FormControl()
  });
  public posts?: IPost[];
  newsParams = new NewsParams();
  totalCount = 0;
  constructor(private _newsService: NewsService
    , private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.GetPosts();
    if (this.currentUser$)
      this.currentUser$.subscribe(user => {
        console.log("user", user);
        if (user?.isAdmin) {
          this.isAdmin = true;
        }
      })
  }
  GetPosts() {
    console.log("params", this.newsParams);
    this._newsService.GetPosts(this.newsParams).subscribe(response => {
      console.log(response);
      if (response) {
        this.posts = response.data;

        for(let post of this.posts){
          post.imageUrl = post.imageUrl.replace("\\","/");
        }

        this.newsParams.PageIndex = response.pageIndex;
        this.newsParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      }

    });
  }
  onPageChanged(event: any) {
    this.newsParams.PageIndex = event.page;
    this.GetPosts();
  }
  onSubmit() {
    this.searchString = this.searchForm.get("search")?.value;
    this.newsParams.search = this.searchForm.get("search")?.value;
    console.log(this.newsParams);
    this.GetPosts();
  }

}
