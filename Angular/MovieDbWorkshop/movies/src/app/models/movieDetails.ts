import { IGenre } from "./IGenre";

export interface IMovieDetails {
    title: string,
    poster_path: string,
    release_date: string,
    genres: IGenre[],
    homepage: string
}