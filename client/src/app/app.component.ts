import { Component, OnInit, ViewChild } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { NgProgressComponent } from "ngx-progressbar";

@Component({
  // tslint:disable-next-line
  selector: "body",
  template: `<ng-progress
      [spinner]="true"
      [thick]="true"
      [color]="'green'"
    ></ng-progress>
    <router-outlet></router-outlet>`,
})
export class AppComponent implements OnInit {
  @ViewChild(NgProgressComponent) progressBar: NgProgressComponent;
  constructor(private router: Router) {}

  ngOnInit() {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }
}
