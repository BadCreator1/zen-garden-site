import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagerComponent } from './components/pager/pager.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SanitizeHtmlPipe } from './components/sanitize-html.pipe';
import { HighlightSearchPipe } from './components/highlight-search.pipe';
import { CropImageComponent } from './components/crop-image/crop-image.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ModalCropImageComponent } from './components/crop-image/modal-crop-image/modal-crop-image.component';
import { MatDialogModule } from '@angular/material/dialog';
import { CommentariesComponent } from './components/commentaries/commentaries.component';
import { MatSidenavModule } from '@angular/material/sidenav';

import { MatIconModule } from '@angular/material/icon'
import { MatButtonModule} from '@angular/material/button';
import { MatListModule } from '@angular/material/list'

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { LatestNewsComponent } from './components/latest-news/latest-news.component';

import {MatCardModule} from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { CarouselComponent } from './components/carousel/carousel.component';

@NgModule({
  declarations: [
    PagerComponent,
    SanitizeHtmlPipe,
    HighlightSearchPipe,
    CropImageComponent,
    ModalCropImageComponent,
    CommentariesComponent,
    LatestNewsComponent,
    CarouselComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    FormsModule,
    ImageCropperModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    RouterModule
  ],
  exports:[
    PaginationModule,
    FormsModule,
    PagerComponent,
    SanitizeHtmlPipe,
    HighlightSearchPipe,
    ImageCropperModule,
    CropImageComponent,
    CommentariesComponent,
    MatSidenavModule,

    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    LatestNewsComponent,
    CarouselComponent
  ]
})

export class SharedModule { }
