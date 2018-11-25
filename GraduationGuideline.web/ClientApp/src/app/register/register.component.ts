import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
    invalidInfo: boolean;

    constructor(private router: Router, private http: HttpClient) { }


    register(form: NgForm) {
        let credentials = JSON.stringify(form.value);
        this.http.post("https://localhost:5001/api/account/register", credentials, {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        }).subscribe(response => {
            let token = (<any>response).token;
            localStorage.setItem("jwt", token);
            this.invalidInfo = false;
            this.router.navigate(["/"]);
        }, err => {
            this.invalidInfo = true;
        });
    }
    
}