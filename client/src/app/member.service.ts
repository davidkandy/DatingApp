import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from './_models/member';

const token: string|null = null;
const userData = localStorage.getItem('user');
const user = JSON.parse(userData) ?? {};

console.log("UserData was", userData);
console.log("User is", user);

const httpOptions = {
  headers: new HttpHeaders({
    // Authorization : 'Bearer' + JSON.parse(localStorage.getItem('user') ?? "").token
	Authorization : 'Bearer' + user.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers()
  {
    return this.http.get<Member[]>(this.baseUrl + 'users', httpOptions);
  }

  getMember(username: string) {
    this.http.get<Member>(this.baseUrl + 'users/' + username, httpOptions);
  }
}
