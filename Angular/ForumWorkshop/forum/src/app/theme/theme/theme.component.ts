import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/user/user.service';
import { ContentService } from '../../content.service';
import { ITheme, IUser } from '../../shared/interfaces';

@Component({
  selector: 'app-theme',
  templateUrl: './theme.component.html',
  styleUrls: ['./theme.component.css', '../../app.component.css']
})
export class ThemeComponent {
  theme: ITheme | undefined;
  currentUser: IUser | undefined
  constructor(
    private contentService: ContentService,
    private activatedRoute: ActivatedRoute,
    private userService: UserService
  ) { 
    this.fetchTheme();
    this.currentUser = this.userService.getUser();
  }

  fetchTheme(): void {
    this.theme = undefined;
    const id = this.activatedRoute.snapshot.params['themeId'];
    this.contentService
      .loadTheme(id)
      .subscribe(theme => this.theme = theme);
  }
}
