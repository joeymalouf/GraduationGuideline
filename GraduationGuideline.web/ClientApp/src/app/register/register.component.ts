import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
    invalidInfo: boolean;

    constructor (private http: HttpClient)  { }
    model: any = {};
    public confirm: string = "";
    public mismatch: boolean = false;
    public types = ["PhD", "Master's Thesis", "Master's Nonthesis"]
    public years = Array.from(Array(23).keys()).map(i => 2018 + i);
    public semesters = ["Spring", "Fall", "Summer"]


    onSubmit() {
        if (this.model.Password == this.confirm) {
            this.mismatch = false;
            this.http.post("/api/auth/create", JSON.stringify(this.model), {
                headers: new HttpHeaders({
                    "Content-Type": "application/json"
                })
            }).subscribe(response => {
                this.invalidInfo = false;
                window.location.href= "https://localhost:5001/login";
            }, err => {
                this.invalidInfo = true;
            });

        }
        else {
            this.mismatch = true;
        }
    }
}