import { Component } from '@angular/core';
import { NavService } from '../services/navService.service';

@Component({
  selector: 'app-steps',
  templateUrl: './steps.component.html',
  styleUrls: ['./steps.component.css']
})
export class StepsComponent {
  public userSteps: Steps[];
  public userStepThrees = [];

  constructor(private _navService: NavService) { 
    
  }

  ngOnInit() {
    for (var i = 0; i < this._navService.steps.length; i += 2) {
      var row = [];
      row.push(this._navService.steps[i])
      while (row.length < 2 && i + row.length < this._navService.steps.length) {
        row.push(this._navService.steps[i + row.length])
      }
      this.userStepThrees.push(row);
    }
  }
}

interface Steps {
  username: string;
  status: boolean;
  stepName: string;
  description: string;
}
