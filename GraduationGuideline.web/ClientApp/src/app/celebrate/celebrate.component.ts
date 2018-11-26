import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NavService } from '../services/navService.service';

@Component({
    selector: 'app-celebrate',
    templateUrl: './celebrate.component.html',
    styleUrls: ['./celebrate.component.css']
})

@Injectable()
export class CelebrateComponent {
    public step: Step;
    statusText = "Incomplete"
    private token: any;
    public deadline: Deadline;

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
        this.getDates();
    }

    getStatus() {
        this.http.get<Step>('api/step/GetStep/Completion', {
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

    getDates() {
        this.http.get<Deadline>('api/step/GetDates', {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json"
            })
        }).subscribe(result => {
            this.deadline = result;
        }, error => {
            console.error(error);
        });
    }

    toggleStatus() {
        this.http.post<Step>('api/step/ToggleStepStatus/Completion', {}, {
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

interface Deadline {
    graduation: any;
    commencement: any;
    hooding: any;
    audit: any;
}