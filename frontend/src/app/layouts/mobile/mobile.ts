import { Component } from '@angular/core';

@Component({
  selector: 'app-layout-mobile',
  template: `<ng-content />`,
  host: {
    class: "max-w-120 p-4 mx-auto flex flex-col h-dvh",
  }
})
export class Mobile {}
