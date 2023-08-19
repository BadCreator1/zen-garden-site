import { Component,EventEmitter,Inject, Output } from '@angular/core';
import { ImageCroppedEvent, LoadedImage } from 'ngx-image-cropper';
import {MatDialog, MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
@Component({
  selector: 'app-modal-crop-image',
  templateUrl: './modal-crop-image.component.html',
  styleUrls: ['./modal-crop-image.component.scss']
})
export class ModalCropImageComponent {
  imageChangedEvent: any = '';
  croppedImage: any = '';
  

  constructor(
    public dialogRef: MatDialogRef<ModalCropImageComponent>,
    @Inject(MAT_DIALOG_DATA) 
    public data: 
    {imageChangedEvent: string, croppedImage: string}){
      this.imageChangedEvent = data.imageChangedEvent;
      this.croppedImage = data.croppedImage;
  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }

  imageLoaded(image?: LoadedImage) {
    console.log("image", image);
    // show cropper
  }

  cropperReady() {
    // cropper ready
    console.log("croped", this.croppedImage);
  }

  loadImageFailed() {
    // show message
  }

}
