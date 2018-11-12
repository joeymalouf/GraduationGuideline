import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  host: {
    "[style.display]": "'block'",
    "[style.width.%]": "100",
  },
})
export class AppComponent {
  title = 'app';
}
