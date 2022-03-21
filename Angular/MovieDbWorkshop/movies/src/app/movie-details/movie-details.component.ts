import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMovieDetails } from '../models/movieDetails';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movie: IMovieDetails | undefined;
  movieId!: string;
  posterPath: string = '';

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) {
    this.movieId = this.route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.movie = undefined;
    this.movieService.getMovie(this.movieId)
      .subscribe(m => this.movie = m);
  }

}
