import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {
  @ViewChild('form') searchForm: NgForm | undefined;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  search() {
    const query = this.searchForm?.value;

    this.router.navigate(['/movies/search'], { queryParams: query });
  }
}
