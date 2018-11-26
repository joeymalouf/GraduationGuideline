import { Component } from '@angular/core';
import { JwtHelper } from 'angular2-jwt';
import { Router } from '@angular/router';

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
  constructor(private jwtHelper: JwtHelper, private router: Router) {
  }

  ngOnInit() {
    this.isUserAuthenticated();
  }

  isUserAuthenticated() {
    let token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
}
