import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  searchTerm: string = '';

  @Output() searchTermChanged = new EventEmitter<string>();

  onSearch(): void {
    this.searchTermChanged.emit(this.searchTerm);
  }
}
