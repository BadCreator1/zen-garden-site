import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'highlightSearch'
})
export class HighlightSearchPipe implements PipeTransform {

  transform(wholeText: string, searchQuery: string): string {
    if (!searchQuery) {
      return wholeText;
    }
    const re = new RegExp(searchQuery, 'gi');
    return wholeText.replace(re, '<mark>$&</mark>');
  }

}
