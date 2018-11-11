import { Component } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
  host: {
    "[style.display]": "'inline-block'",
    "[style.width.%]": "100",
    "[style.height.%]": "100",
  },
})


export class FooterComponent {
  steps = [
      { stepName: "Stuff1", status: true },
      { stepName: "Stuff2", status: false },
      { stepName: "Stuff3", status: true },
      { stepName: "Stuff4", status: true },
      { stepName: "Stuff5", status: false },
  ];
}
