import { Component, Inject } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { TargetLocator } from 'selenium-webdriver';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelper } from 'angular2-jwt';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  

  public userSteps: Steps[];
  private username: string;

  constructor(private jwtHelper: JwtHelper, private router: Router, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    
  }

  ngOnInit() {
    let token = localStorage.getItem("jwt");
    this.http.get<string>('api/auth/currentUser', {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this.username = result;
    }, error => {
      console.error(error);
    });
    
    this.http.get<Steps[]>('api/nav/GetStepsByUsername/' + this.username, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this.userSteps = result;
    }, error => console.error(error));
  }
  collapse = false;
  
  isUserAuthenticated() {
    let token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  logOut() {
    localStorage.removeItem("jwt");
    this.router.navigate(["login"]);
 }

  close() {
    this.collapse = false;
    
  }

  toggle() {
    this.collapse = !this.collapse;
  }
}

interface Steps {
  username: string;
  status: boolean;
  stepName: string;
}