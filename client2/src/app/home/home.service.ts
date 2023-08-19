import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { IPost } from '../shared/models/IPost';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  GetPopularPosts(){
    return this.http.get<IPost[]>(this.baseUrl + 'Posts/Popular');
  }
  GetLatestPosts(){
    return this.http.get<IPost[]>(this.baseUrl + 'Posts/latest');
  }
}
