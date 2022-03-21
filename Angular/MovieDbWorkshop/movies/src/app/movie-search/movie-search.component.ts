import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IMovie } from '../models/movie';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-movie-search',
  templateUrl: './movie-search.component.html',
  styleUrls: ['./movie-search.component.css']
})
export class MovieSearchComponent implements OnInit {
  searchedMovies: IMovie[] | undefined;
  query: string = '';

  constructor(
    private movieService: MovieService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.query = this.activatedRoute.snapshot.queryParams['search'];
    this.movieService.searchMovie(this.query)
      .subscribe((data) => {
        this.searchedMovies = data['results'];
      });
  }
}
