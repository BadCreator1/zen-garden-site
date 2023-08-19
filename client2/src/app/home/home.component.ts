import { Component, OnInit } from '@angular/core';
import { HomeService } from './home.service';
import { IPost } from '../shared/models/IPost';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{

  posts: IPost[] = [];
  noWrapSlides = false;
  showIndicator = true;
  activeSlideIndex = 0;
  constructor(private _homeService: HomeService) { }
  ngOnInit(): void {
    this.getPopularPosts();
  }

  getPopularPosts(){
    this._homeService.GetPopularPosts().subscribe(response => {
      console.log(response);
      this.posts = response.reverse();
    });
  }
 
}
