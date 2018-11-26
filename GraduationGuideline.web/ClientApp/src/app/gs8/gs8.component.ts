import { Component, Inject, Injectable } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { basename } from 'path';
import { JwtHelper } from 'angular2-jwt';
import { StepsComponent } from '../steps/steps.component';
import { NavService } from '../services/navService.service';

@Component({
  selector: 'app-gs8',
  templateUrl: './gs8.component.html',
  styleUrls: ['./gs8.component.css']
})

@Injectable()
export class Gs8Component {
  public step: Step;
  deadline: any;
  statusText = "Incomplete"
  private token: any;

  constructor(private http: HttpClient, private _navService: NavService) {

  }

  ngOnInit() {
    this.token = localStorage.getItem("jwt");
    this.getStatus();
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
    this.http.get<Step>('api/step/GetStep/GS8', {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + this.token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this.step = result;
      this.getStatusDescription();
    }, error => {
      console.error(error);
    });
  }

  toggleStatus() {
    this.http.post<Step>('api/step/ToggleStepStatus/GS8', {}, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + this.token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this.step = result;
      this.getStatusDescription();
      this._navService.toggleStep(result.stepName);
    }, error => {
      console.error(error);
    });
  }
}

interface Step {
  username: string;
  status: boolean;
  stepName: string;
  deadline: any;
}
