import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { IPost } from '../shared/models/IPost';
import { NewsParams } from '../shared/models/newsParams';
import { IPagination } from '../shared/models/Pagination';
import { BehaviorSubject, map } from 'rxjs';
import { IUser } from '../shared/models/IUser';
@Injectable({
  providedIn: 'root'
})
export class NewsService {

  baseUrl = environment.apiUrl;

  

  constructor(private http: HttpClient) { }

  public GetPosts(newsParams: NewsParams) {
    let params = new HttpParams();
    params = params.append('pageIndex', newsParams.PageIndex.toString());
    params = params.append('pageSize', newsParams.pageSize.toString());
    if (newsParams.search)
      params = params.append('search', newsParams.search.toString());
    return this.http.get<IPagination>(this.baseUrl + 'Posts', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  public GetPost(id: number) {
    return this.http.get<IPost>(this.baseUrl + 'Posts/' + id);
  }

  public UpdatePost(post: IPost) {
    return this.http.post<IPost>(this.baseUrl + 'Posts', post);
  }
  public DeletePost(id: number) {
    return this.http.delete(this.baseUrl + 'Posts/' + id);
  }

  public UploadImage(formData: FormData){
    return this.http.post(this.baseUrl + 'Files', formData,  {reportProgress: true, observe: 'events'});
  }
  GetPopularPosts(){
    return this.http.get<IPost[]>(this.baseUrl + 'Posts/Popular');
  }
  GetLatestPosts(){
    return this.http.get<IPost[]>(this.baseUrl + 'Posts/latest');
  }
}
