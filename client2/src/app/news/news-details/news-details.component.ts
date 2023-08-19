import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BlockTypes } from 'src/app/shared/enums/block-types';
import { IPost } from 'src/app/shared/models/IPost';
import { NewsService } from '../news.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Editor, Toolbar, Validators, toDoc, toHTML } from 'ngx-editor';
import { FormControl, FormGroup } from '@angular/forms';
import { SafeHtml } from '@angular/platform-browser';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { IFile } from 'src/app/shared/models/IFile';
import { AccountService } from 'src/app/account/account.service';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/IUser';
@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.scss',]
})
export class NewsDetailsComponent implements OnInit, OnDestroy {
  public editor?: Editor;
  public post?: IPost;
  public html = "";
  public isEdit = false;
  public isAdmin = false;
  toolbar: Toolbar = [
    ['bold', 'italic'],
    ['underline', 'strike'],
    ['code', 'blockquote'],
    ['ordered_list', 'bullet_list'],
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['link', 'image'],
    ['text_color', 'background_color'],
    ['align_left', 'align_center', 'align_right', 'align_justify'],
  ];

  form = new FormGroup({
    editorContent: new FormControl('', Validators.required()),
  });
  progress?: number;
  message?: string;
  currentUser$?: Observable<IUser | null>;

  constructor(private _newsService: NewsService,
    private route: ActivatedRoute
    , private router: Router
    , private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.editor = new Editor();
    this.route.params.subscribe(params => {
      const id = +params['id'];
      if (id > 0)
        this._newsService.GetPost(params['id']).subscribe(post => {
          console.log(post);
          if (post) {
            console.log(toDoc(post.jsonDoc));
            this.post = post;
            this.html = post.jsonDoc ? post.jsonDoc : this.TitleAndUrlToHtml(post);
            this.form.get("editorContent")?.setValue(this.html);
          }

        });
      else {
        this.post = {
          id: 0,
          title: "",
          imageUrl: "",
          userId: undefined,
          blocks: [],
          commentaries: [],
          views: 0,
          jsonDoc: "",
          description: ""
        }
      }
    });
    this.currentUser$.subscribe(user => {
      console.log("user", user);
      this.isEdit = false;
      if(user?.isAdmin){
        this.isEdit = true;
        this.isAdmin = true;
      }
    })
    
  }

  onSave() {
    if (this.form.get("editorContent")?.value) {
      this.html = this.form.get("editorContent")?.value?.toString() ?? "";
      console.log(this.html);
      if (this.post) {

        this.post.jsonDoc = this.html;
        var jsonDoc = toDoc(this.html);
        this.post.title = this.findTitle(jsonDoc);
        this.post.imageUrl = this.findImageUrl(jsonDoc);
        this.post.description = this.findTextValue(jsonDoc);
        this._newsService.UpdatePost(this.post).subscribe(response => {
          console.log(response);
          this.router.navigate(['news/' + response.id]);
        }, error => console.log(error))
      }


    }

  }

  addImageUrl(url: string) {
    let editorContent = this.form.get("editorContent");
    if (editorContent) {
      this.html = editorContent.value?.toString() ?? "";
      this.html += `<img src='${url}' />`;
      editorContent.setValue(this.html);
    }


  }

  findTitle(jsonDoc: Record<string, any>) {
    if (jsonDoc['content']) {
      for (const item of jsonDoc['content']) {
        if (item.type === "heading" && item.content.length > 0) {
          return item.content[0].text;
        }
      }
    }
  }
  findImage(jsonDoc: Record<string, any>): Record<string, any> | null {
    if (jsonDoc['content']?.length > 0) {
      for (const item of jsonDoc['content']) {
        if (item.type === "image") {
          return item;
        }
        else if (item.content?.length > 0) {

          var image = this.findImage(item);
          if (image) {
            return image;
          }
        }
      }
    }
    return null;
  }
  findText(jsonDoc: Record<string, any>): Record<string, any> | null {
    if (jsonDoc['content']?.length > 0) {
      for (const item of jsonDoc['content']) {
        if (item.type == 'heading')
          continue;
        if (item.type === "text") {
          return item;
        }
        else if (item.content?.length > 0) {
          var text = this.findText(item);
          if (text) {
            return text;
          }
        }
      }
    }
    return null;
  }

  findTextValue(jsonDoc: Record<string, any>) {
    var text = this.findText(jsonDoc);
    console.log(text);
    return text ? text['text'] : ""
  }

  findImageUrl(jsonDoc: Record<string, any>) {
    const image = this.findImage(jsonDoc);
    if (image) {
      return image['attrs']['src'].toString();
    }
    return "";
  }
  TitleAndUrlToHtml(post: IPost): string {
    let html = `<h1>${post.title}</h1><img src='${post.imageUrl}'>`;
    for (const item of post.blocks) {
      if (item.blockTypeId == BlockTypes.Text) {
        html += `<p>${item.content}</p>`;
      }
      if (item.blockTypeId == BlockTypes.Image) {
        html += `<img src='${item.content}'>`;
      }

    }
    return html;
  }

  uploadFile = (files: any) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this._newsService.UploadImage(formData)
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress && event.total)
            console.log("loading: ", Math.round(100 * event.loaded / event.total));
          else if (event.type === HttpEventType.Response) {
            console.log('Upload success.');
            console.log("url?: ", event.body);
            let url = event.body as IFile;
            if (url)
              this.addImageUrl(url.resultPath);
          }
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });
  }

  ngOnDestroy(): void {
    this.editor?.destroy();
  }

  editChange(event: any) {
    //this.isEdit = !this.isEdit;
    console.log(event.target.value);
  }

  onDelete() {
    if (this.post && this.post?.id > 0)
      this._newsService.DeletePost(this.post.id).subscribe(response => {
        console.log(response);
        this.router.navigate(['/news']);
      },
        error => console.log(error));
  }
}
