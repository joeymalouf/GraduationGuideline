import { Component, Inject, Injectable } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { basename } from 'path';

@Component({
  selector: 'app-gs8',
  templateUrl: './gs8.component.html',
  styleUrls: ['./gs8.component.css']
})

@Injectable()
export class Gs8Component {
  public step: Step;
  private headers: HttpHeaders;
  deadline = ""
  statusText = "Incomplete"

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

    this.getStatus()

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
    this.http.get<Step>('api/nav/GetStep/').subscribe(result => {
      this.step = result;
      return this.step;
    }, error => {
      console.error(error);
      this.step = {
        username: "jmmalouf",
        stepName: "GS8",
        status: true,
      }
      this.deadline = "11/26/18"
      if (this.step.status) {
        this.statusText = "Complete"
      }
    });
    return;

    // http.get<Steps[]>(baseUrl + 'api/nav/GetStepsByUsername/jmmalouf').subscribe(result => {
    //   this.userSteps = result;
    // }, error => console.error(error));
  }

  toggleStatus() {
    var payload = {username: "jmmalouf", stepName: "GS8"}
    this.http.post<boolean>('api/step/ToggleStepStatus', payload, {headers: this.headers}).subscribe(result => {
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
