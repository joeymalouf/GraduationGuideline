import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
    selector: 'app-account',
    templateUrl: './account.component.html',
})
export class AccountComponent {
    invalidInfo: boolean;

    constructor(private http: HttpClient) { }
    model: any = {};
    public types = ["PhD", "Master's Thesis", "Master's Nonthesis"]
    public years = Array.from(Array(23).keys()).map(i => 2018 + i);
    public semesters = ["Spring", "Fall", "Summer"]
    private token: any;

    ngOnInit() {
        this.token = localStorage.getItem("jwt");
        this.http.get("/api/accounts/userdata", {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json"
            })
        }).subscribe(response => {
            this.model = response
            this.invalidInfo = false;
        }, err => {
            console.error(err);
        });
    }

    onSubmit() {
        this.http.post("/api/accounts/update", JSON.stringify(this.model), {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + this.token,
                "Content-Type": "application/json"
            })
        }).subscribe(response => {
            this.invalidInfo = false;

        }, err => {
            console.error(err);
        });

    }
}