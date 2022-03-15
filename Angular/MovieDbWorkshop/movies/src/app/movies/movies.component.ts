import { Component, OnInit } from '@angular/core';
import { IMovie } from '../models/movie';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  popularMovies: IMovie[] | undefined;
  inTheaterMovies: IMovie[] | undefined;
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.popularMovies = undefined;
    this.movieService.getPopularMovies()
      .subscribe(data => {
        this.popularMovies = data['results'].slice(0, 6);
      });
    
    this.inTheaterMovies = undefined;
    this.movieService.getMoviesInTheater()
      .subscribe(data => this.inTheaterMovies = data['results'].slice(0, 6));
  }

}
