import { Component, Inject } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { HttpClient } from '@angular/common/http';
import { NavService } from '../services/navService.service';

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

  constructor(private _navService: NavService) {
    
  }
}

interface Steps {
  username: string;
  status: boolean;
  stepName: string;
}
