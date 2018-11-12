import { Component, Inject } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
  host: {
    "[style.display]": "'block'",
    "[style.width.%]": "100",
    "[style.height.%]": "100",
  },
})


export class FooterComponent {
  public userSteps: Steps[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Steps[]>(baseUrl + 'api/Footer/GetStepsByUsername/jmmalouf').subscribe(result => {
      this.userSteps = result;
    }, error => console.error(error));
  }
}

interface Steps {
  username: string;
  status: boolean;
  stepName: string;
}
