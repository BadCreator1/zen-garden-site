import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ImageCroppedEvent, LoadedImage } from 'ngx-image-cropper';
import { ModalCropImageComponent } from './modal-crop-image/modal-crop-image.component';

@Component({
  selector: 'app-crop-image',
  templateUrl: './crop-image.component.html',
  styleUrls: ['./crop-image.component.scss']
})
export class CropImageComponent {
  imageChangedEvent: any = '';
  croppedImage: any = '';
  @Output() onImageCropped = new EventEmitter<any>();

  constructor(public dialog: MatDialog) {

  }
  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    this.openDialog();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ModalCropImageComponent, {
      data: {imageChangedEvent: this.imageChangedEvent, croppedImage: this.croppedImage},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.croppedImage = result;
      this.onImageCropped.emit(result);
    });
  }
}
