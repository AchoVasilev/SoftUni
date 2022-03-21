import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/interfaces';
import { UserService } from '../user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user: IUser | undefined;
  constructor(private userService: UserService) {
    this.getUserData();
  }

  ngOnInit(): void {
  }

  getUserData(): void {
    this.user = this.userService.getUser();
  }
}
