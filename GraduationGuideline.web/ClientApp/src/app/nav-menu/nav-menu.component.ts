import { Component, Inject } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { TargetLocator } from 'selenium-webdriver';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelper } from 'angular2-jwt';
import { Router } from '@angular/router';
import { NavService } from '../services/navService.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
  

  constructor(private jwtHelper: JwtHelper, private router: Router, private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private _navService: NavService) {

    
  }

  ngOnInit() {
    let token = localStorage.getItem("jwt");

    this.http.get<Steps[]>('api/step/GetCurrentUserSteps/', {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this._navService.setSteps(result);
    }, error => console.error(error));
  }
  collapse = false;

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