import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMovie } from '../models/movie';
import { IMovieDetails } from '../models/movieDetails';

const baseUrl = 'https://api.themoviedb.org/3';
const movieUrl = '/movie';
const discoverUrl = '/discover' + movieUrl;
const apiKey = '?api_key=dcac1671a1d3c62705912fa8952643ca';

const bestDramaMovies = '&with_genres=18&primary_release_year=2021';
const kidsMoviesUrl = '&certification_country=US&certification.lte=G&sort_by=popularity.desc';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

  getPopularMovies(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(baseUrl + movieUrl + '/popular' + apiKey);
  }

  getMoviesInTheater(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(baseUrl + discoverUrl + apiKey + '&with_release_type=2|3');
  }

  getPopularKidsMovies(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(baseUrl + discoverUrl + apiKey + kidsMoviesUrl);
  }

  getBestDramaMovies(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(baseUrl + discoverUrl + apiKey + bestDramaMovies);
  }

  getMovie(movieId: string): Observable<IMovieDetails> {
    return this.http.get<IMovieDetails>(baseUrl + movieUrl + '/' + movieId + apiKey);
  }

  searchMovie(query: string) {
    return this.http.get<IMovie[]>(baseUrl + '/search/movie' + apiKey + `&query=${query}`);
  }
}
