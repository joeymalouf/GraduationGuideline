import { Component, Inject, Injectable } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { basename } from 'path';
import { JwtHelper } from 'angular2-jwt';
import { StepsComponent } from '../steps/steps.component';

@Component({
  selector: 'app-gs8',
  templateUrl: './gs8.component.html',
  styleUrls: ['./gs8.component.css']
})

@Injectable()
export class Gs8Component {
  public step: Step;
  private headers: HttpHeaders;
  deadline: any;
  statusText = "Incomplete"
  private token: any;

  constructor(private jwtHelper: JwtHelper, private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });


  }

  ngOnInit() {
    this.token = localStorage.getItem("jwt");
    this.getStatus();
    this.getStatusDescription();
  }

  getStatusDescription() {
    if (this.step.status) {
      this.statusText = "Complete"
    }
    else {
      this.statusText = "Incomplete"
    }
  }

  getStatus() {
    this.http.get<Step>('api/nav/GetStep/GS8').subscribe(result => {
      this.step = result;
    }, error => {
      console.error(error);
    });
    return;
  }

  toggleStatus() {
    this.http.get<boolean>('api/step/ToggleStepStatus/GS8', {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + this.token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this.step.status = result;
      this.step.status = !this.step.status;
      this.getStatusDescription()
    }, error => {
      console.error(error);
    });
  }
}

interface Step {
  username: string;
  status: boolean;
  stepName: string;
}
