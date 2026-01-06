import { Component, inject, type OnInit } from "@angular/core"
import { Router } from "@angular/router"
import { AppRoutes } from "#/app/app.routes"
import { Constants } from "#/app/constants"
import { OfflineStorageProvider } from "#/app/features/storage/offline-storage-provider"

@Component({
  selector: "app-home",
  template: ``,
})
export class Home implements OnInit {
  private readonly router = inject(Router)
  private readonly localStorageProvider = inject(OfflineStorageProvider)

  ngOnInit() {
    const result = this.localStorageProvider.load<boolean>(
      Constants.Storage.UserIsOld,
    )

    if (!result.isOk) console.warn("Failed to load from storage:", result.error)

    if (result.isOk && result.value === true) {
      this.router.navigateByUrl(AppRoutes.curriculum())
    } else {
      this.router.navigateByUrl(AppRoutes.intro())
    }
  }
}
