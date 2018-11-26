import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NavService } from '../services/navService.service';

@Component({
  selector: 'app-diplomaApp',
  templateUrl: './diplomaApp.component.html',
  styleUrls: ['./diplomaApp.component.css']
})

@Injectable()
export class DiplomaAppComponent {
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
    this.http.get<Step>('api/step/GetStep/Diploma%20App', {
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
    this.http.post<Step>('api/step/ToggleStepStatus/Diploma%20App', {}, {
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
}
