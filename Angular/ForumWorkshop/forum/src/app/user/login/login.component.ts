import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router) { }

  login(email:string, password: string): void{
    this.userService.login(email, password);
    const redirectUrl = this.activatedRoute.snapshot.queryParams['redirectUrl'] || '/';

    this.router.navigate([redirectUrl]);
  }
}
