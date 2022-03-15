import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMovie } from '../models/movie';

const baseUrl = 'https://api.themoviedb.org/3';
const movieUrl = '/movie';
const inTheaters = '/discover' + movieUrl;
const apiKey = '?api_key=dcac1671a1d3c62705912fa8952643ca';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

  getPopularMovies(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(baseUrl + movieUrl + '/popular' + apiKey);
  }

  getMoviesInTheater(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(baseUrl + inTheaters + apiKey + '&with_release_type=2|3');
  }
}
