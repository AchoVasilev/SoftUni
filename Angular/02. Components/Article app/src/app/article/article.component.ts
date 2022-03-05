import { Component, Input, OnInit } from '@angular/core';
import { Article } from '../models/article.models';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  private symbols: number = 250;

  constructor() {
    this.articleDescLen = 0;
    this.descToShow = '';
  }

  ngOnInit(): void {
  }

  @Input() article: Article = new Article('', '', '', '');
  @Input() articleDesc: string = '';

  descToShow: string;
  articleDescLen: number;
  showReadMoreBtn: boolean = true;
  showHideBtn: boolean = false;
  imageIsShown: boolean = false;
  imageButtonTitle: string = 'Show Image';

  readMore(): void {
    this.articleDescLen += this.symbols;
    if (this.articleDescLen >= this.articleDesc.length) {
      this.showHideBtn = true;
      this.showReadMoreBtn = false;
    } else {
      this.descToShow = this.articleDesc.substring(0, this.articleDescLen);
    }
  }

  hideDesc() {
    this.descToShow = '';
    this.articleDescLen = 0;
    this.showHideBtn = false;
    this.showReadMoreBtn = true;
  }

  toggleImage(): void {
    this.imageIsShown = !this.imageIsShown;
    this.imageButtonTitle = this.imageButtonTitle == 'Show Image' ? 'Hide Image' : ' Show Image';
  }
}
