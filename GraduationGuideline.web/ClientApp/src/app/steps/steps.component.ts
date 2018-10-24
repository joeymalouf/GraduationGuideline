import { Component } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';

@Component({
  selector: 'app-steps',
  templateUrl: './steps.component.html',
  styleUrls: ['./steps.component.css']
})
export class StepsComponent {
  isExpanded = false;

  steps = ["one", "two", "three"];
  
  collapse() {
    this.isExpanded = false;
    
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
