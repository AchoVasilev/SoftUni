import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IPost, ITheme } from './shared/interfaces';

const apiUrl = environment.apiURL;

@Injectable({
  providedIn: 'root'
})
export class ContentService {

  constructor(private http: HttpClient) { }

  loadTheme(id: string) {
    return this.http.get<ITheme>(`${apiUrl}/themes/${id}`);
  }

  loadThemes() {
    return this.http.get<ITheme[]>(`${apiUrl}/themes`);
  }

  loadPosts(limit?: number) {
    const query = limit ? `?limit=${limit}` : '';
    return this.http.get<IPost[]>(`${apiUrl}/posts${query}`);
  }
}
