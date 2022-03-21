import { Component } from '@angular/core';
import { UserService } from 'src/app/user/user.service';
import { ContentService } from '../../content.service';
import { IPost, ITheme } from '../../shared/interfaces';

@Component({
  selector: 'app-theme',
  templateUrl: './themes.component.html',
  styleUrls: ['../../app.component.css', './themes.component.css']
})
export class ThemesComponent {
  themes: ITheme[] | undefined;
  recentPosts: IPost[] | undefined;

  get isLogged(): boolean {
    return this.userService.isLogged;
  }

    constructor(private contentService: ContentService, private userService: UserService) {
    this.fetchThemes();
    this.fetchRecentPosts();
  }

  fetchThemes(): void {
    this.themes = undefined;
    this.contentService
      .loadThemes()
      .subscribe(themes => this.themes = themes);
  }

  fetchRecentPosts(): void {
    this.recentPosts = undefined;
    this.contentService
      .loadPosts(5)
      .subscribe(posts => this.recentPosts = posts);
  }
  
  isSubscribed(id: string): boolean {
    const elements = this.themes?.filter(t => t._id == id && t.subscribers.includes('5fa64b162183ce1728ff371d'));
    let isTrue = false;

    if (elements) {
      isTrue = elements.length > 0 ? true : false;
    }

    return isTrue;
  }
}
