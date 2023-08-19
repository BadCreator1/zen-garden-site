import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {
  @Input() totalCount: number = 0;
  @Input() pageSize: number = 0;
  @Input() pageNumber?: number;
  @Output() pageChanged = new EventEmitter<number>();
  
  onPagerChange(event: any) {
    this.pageChanged.emit(event);
  }
}
