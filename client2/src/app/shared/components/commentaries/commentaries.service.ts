import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICommentary } from '../../models/ICommentary';
import { environment } from 'src/environments/environment.development';
import { CommentParams } from '../../models/commentParams';
import { IPagination } from '../../models/Pagination';
import { map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CommentariesService {
  
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetCommentaries(spec: CommentParams){
    let params = new HttpParams();
    params = params.append('pageIndex', spec.PageIndex.toString());
    params = params.append('pageSize', spec.pageSize.toString());
    if (spec.postId)
      params = params.append('postId', spec.postId.toString());

    return this.http.get<IPagination>(this.baseUrl + 'Comments', { observe: 'response', params })
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

 
  UpdateCommentary(comment: ICommentary){
    console.log("commentary", comment);
    return this.http.post(this.baseUrl + 'Comments', comment);
  }

  DeleteCommentary(id: number){
    return this.http.delete(this.baseUrl + 'Comments?id=' + id);
  }
}
