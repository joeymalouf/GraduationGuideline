import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  userInfo: any;

  constructor(private http: HttpClient) { }
 
  ngOnInit() {
    let token = localStorage.getItem("jwt");
    this.http.get("https://localhost:5001/api/admin/AllUserData", {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      this.userInfo = response;
    }, err => {
      console.log(err)
    });
  }
}
