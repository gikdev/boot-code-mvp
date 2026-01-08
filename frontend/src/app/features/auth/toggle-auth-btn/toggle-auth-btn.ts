import { Component, computed, inject } from "@angular/core"
import { NgIcon, provideIcons } from "@ng-icons/core"
import {
  phosphorLock,
  phosphorLockKeyOpen,
} from "@ng-icons/phosphor-icons/regular"
import { Constants } from "#/app/constants"
import { Auth } from "../auth"

@Component({
  selector: "app-toggle-auth-btn",
  imports: [NgIcon],
  providers: [provideIcons({ phosphorLock, phosphorLockKeyOpen })],
  template: `
    <button (click)="toggleAdmin()" hlmBtn variant="ghost" size="icon-sm" class="absolute top-2 left-2 cursor-pointer inline-flex gap-1 items-center">
      @if (isAdmin()) {
        <ng-icon name="phosphorLock" />
      } @else {
        <ng-icon name="phosphorLockKeyOpen" />
      }
      <span class="text-xs">v{{appVersion}}</span>
    </button>
  `,
})
export class ToggleAuthBtn {
  private auth = inject(Auth)

  protected appVersion = Constants.AppVersion
  protected isAdmin = computed(() => this.auth.isAdmin())
  protected toggleAdmin = () => {
    const isAdmin = this.auth.isAdmin()

    if (isAdmin) {
      this.auth.logout()
      return
    }

    this.auth.askForLogin()
  }
}
