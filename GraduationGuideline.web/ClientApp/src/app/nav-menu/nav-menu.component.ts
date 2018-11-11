import { Component } from '@angular/core';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';
import { TargetLocator } from 'selenium-webdriver';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  collapse = false;
  
  steps = ['one', 'two']

  close() {
    this.collapse = false;
    
  }

  toggle() {
    this.collapse = !this.collapse;
  }
  
  fold(target) {
    
  }
}
