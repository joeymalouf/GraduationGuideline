import { Component, Inject } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { TargetLocator } from 'selenium-webdriver';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  

  public userSteps: Steps[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Steps[]>(baseUrl + 'api/nav/GetStepsByUsername/jmmalouf').subscribe(result => {
      this.userSteps = result;
    }, error => console.error(error));
  }

  collapse = false;
  

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