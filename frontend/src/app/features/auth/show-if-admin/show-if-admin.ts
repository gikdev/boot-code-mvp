import { Component, computed, inject, input } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import {
  phosphorHouse,
  phosphorLockOpen,
} from "@ng-icons/phosphor-icons/regular"
import { AppRoutes } from "#/app/app.routes"
import { HlmButtonImports } from "#/libs/ui/button/src"
import { Auth } from "../auth"

@Component({
  selector: "app-show-if-admin",
  imports: [NgIcon, RouterLink, HlmButtonImports],
  providers: [provideIcons({ phosphorHouse, phosphorLockOpen })],
  template: `
    @if (isAdmin()) {
      <ng-content />
    } @else if (showFallback()) {
      <div class="flex flex-col items-center justify-center text-center gap-2">
        <i class="ph-fill ph-warning-circle text-3xl text-destructive"></i>
        <p class="text-destructive">شما دسترسی کافی ندارید.</p>

        <a hlmBtn variant="outline" [routerLink]="getHomePage()">
          <span>بازگشت به خانه</span>
          <ng-icon name="phosphorHouse" />
        </a>

        <button hlmBtn variant="default" (click)="askForAdmin()">
          <span>‌ادمین هستم</span>
          <ng-icon name="phosphorLockOpen" />
        </button>
      </div>
    } @else {
      <!-- NOTHING! -->
    }
  `,
})
export class ShowIfAdmin {
  private auth = inject(Auth)
  public showFallback = input.required<boolean>()
  protected isAdmin = computed(() => this.auth.isAdmin())
  protected getHomePage = () => AppRoutes.home()
  protected askForAdmin = () => this.auth.askForLogin()
}
