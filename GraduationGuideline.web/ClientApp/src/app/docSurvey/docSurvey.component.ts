import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NavService } from '../services/navService.service';

@Component({
  selector: 'app-docSurvey',
  templateUrl: './docSurvey.component.html',
  styleUrls: ['./docSurvey.component.css']
})

@Injectable()
export class DoctorateSurveyComponent {
  public step: Step;
  deadline: any;
  statusText = "Incomplete"
  private token: any;
  public unsetDate = false;

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
    this.http.get<Step>('api/step/GetStep/Survey of Earned Doctorates', {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + this.token,
        "Content-Type": "application/json"
      })
    }).subscribe(result => {
      this.step = result;
      this.getStatusDescription();
      if (this.step.deadline == "2015-12-31T18:00:00") {
        this.unsetDate = true;
      } 
    }, error => {
      console.error(error);
    });
  }

  toggleStatus() {
    this.http.post<Step>('api/step/ToggleStepStatus/Survey of Earned Doctorates', {}, {
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
