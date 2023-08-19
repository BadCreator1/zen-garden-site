import { Component } from '@angular/core';
import { IPost } from '../../models/IPost';
import { HomeService } from 'src/app/home/home.service';
import { animate, keyframes, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
  animations: [
    trigger('carouselAnimation', [
      transition('void => *', [
        style({ opacity: 0 }),
        animate('300ms', style({ left: 0 }))
      ]),
      transition('* => void', [
        animate('300ms', style({ left: -100 }))
      ])
    ])
  ]
})
export class CarouselComponent {
  posts: IPost[] = [];
  activePost?: IPost;
  activeSlideIndex = 0;
  constructor(private _homeService: HomeService) { }
  ngOnInit(): void {
    console.log("carousel starts");

    this.getPopularPosts();
  }

  getPopularPosts() {
    this._homeService.GetPopularPosts().subscribe(response => {
      console.log(response);
      this.posts = response.reverse();
      if (this.posts.length > 0) {
        this.activePost = this.posts[0];
      }
    });
  }

  carouselBtnClicked(isNext = false) {
    console.log(isNext);
    
    if (isNext) {
      this.activeSlideIndex = this.activeSlideIndex == this.posts.length - 1 ? 0:  this.activeSlideIndex + 1;
    }
    else {
      this.activeSlideIndex = this.activeSlideIndex < 1 ? this.posts.length - 1 : this.activeSlideIndex-1;
    }
    console.log(this.activeSlideIndex);
    
    this.activePost = this.posts[this.activeSlideIndex];
    console.log(this.activePost);
    
  }

  
}
