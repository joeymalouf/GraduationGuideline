import { Component, Inject } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { HttpClient } from '@angular/common/http';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-steps',
  templateUrl: './steps.component.html',
  styleUrls: ['./steps.component.css']
})
export class StepsComponent {
  public userSteps: Steps[];
  public userStepThrees = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Steps[]>(baseUrl + 'api/nav/GetStepsByUsername/jmmalouf').subscribe(result => {
      this.userSteps = result;
      if (this.userSteps) {
        for (var i = 0; i < this.userSteps.length; i += 2) {
          var step = this.userSteps[i];
          var row = [];
          row.push(this.userSteps[i])
          while (row.length < 2 && i + row.length < this.userSteps.length) {
            row.push(this.userSteps[i + row.length])
          }
          this.userStepThrees.push(row);
        }
      }
    }, error => console.error(error));

    
  }
}

interface Steps {
  username: string;
  status: boolean;
  stepName: string;
  description: string;
}
