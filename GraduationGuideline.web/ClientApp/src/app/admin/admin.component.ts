import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: 'app-admin',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css']
})

@Injectable()
export class AdminComponent {
    statusText = "Incomplete"
    private token: any;
    public deadlines: any;
    public users: any;
    public addDeadline: Deadline = {
        semester: "",
        year: 0,
        gS8: "",
        proQuest: "",
        graduation: "",
        commencement: "",
        hooding: "",
        audit: "",
    }
    public searchData: SearchData = {
        semester: "",
        year: 0,
    };
    public years = [0].concat(Array.from(Array(23).keys()).map(i => 2018 + i));
    public semesters = ["", "Spring", "Fall", "Summer"]

    constructor(private http: HttpClient) {

    }

    ngOnInit() {
        this.token = localStorage.getItem("jwt");
        this.getUsersAndSteps();
        this.getDeadlines();
    }

    getUsersAndSteps() {
        this.http.post<any>('api/admin/AllUserData', this.searchData, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json"
            })
        }).subscribe(result => {
            this.users = result;
        }, error => {
            console.error(error);
        });
    }

    getDeadlines() {
        this.http.get<any>('api/admin/GetDeadlines', {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json"
            })
        }).subscribe(result => {
            this.deadlines = result;
        }, error => {
            console.error(error);
        });
    }
    AddDeadline(){
        this.http.post<any>('api/admin/CreateDeadline', this.addDeadline, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json"
            })
        }).subscribe(result => {
            this.getDeadlines()
        }, error => {
            console.error(error);
        });
    }
}

interface SearchData {
    semester: string,
    year: number
}

interface Deadline {
    semester: string,
    year: number,
    gS8: string,
    proQuest: string,
    graduation: string,
    commencement: string,
    hooding: string,
    audit: string,
}