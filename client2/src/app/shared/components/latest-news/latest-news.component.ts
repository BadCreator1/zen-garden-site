import { Component, OnInit } from '@angular/core';
import { IPost } from '../../models/IPost';
import { NewsService } from 'src/app/news/news.service';

@Component({
  selector: 'app-latest-news',
  templateUrl: './latest-news.component.html',
  styleUrls: ['./latest-news.component.scss']
})
export class LatestNewsComponent implements OnInit {
  posts: IPost[] = [];
  public descriptionLength = 100;
  constructor(private _newsService: NewsService){

  }
  ngOnInit(): void {
    this._newsService.GetPopularPosts().subscribe(response => {
      console.log("latest", response)
      this.posts = response;
    })
  }
}
