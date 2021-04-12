import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  constructor( private accountService: AccountService) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const item = localStorage.getItem('user') ?? "";
    if(item){
      const user: User = JSON.parse(item);
      this.accountService.setCurrentUser(user)
    }
  }

  // THIS IS THE ORIGINAL SETCURRENTUSER FUNCTION
  /*
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if(user){
      this.accountService.setCurrentUser(user);
    }
  }
  */

  // REMOVED
  /*
  getUsers() {
    this.http.get('https://localhost:5001/api/users')
    .subscribe(response =>
    {
      this.users = response;
    }, error=>
    {
      console.log(error);
    } 
    );
  }
  */

}
