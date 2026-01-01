import { Component } from "@angular/core"
import { ToggleAuthBtn } from "#/app/features/auth/toggle-auth-btn/toggle-auth-btn"

@Component({
  selector: "app-mobile",
  imports: [ToggleAuthBtn],
  host: { class: "layout-phone" },
  template: `
    <app-toggle-auth-btn />
    <ng-content />
  `,
})
export class Mobile {}
