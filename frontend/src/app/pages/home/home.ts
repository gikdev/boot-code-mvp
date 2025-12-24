import { Component, inject, type OnInit } from "@angular/core"
import { Router } from "@angular/router"
import { LocalStorageProvider } from "#/app/common/local-storage-provider.service"
import { Constants } from "#/app/constants"

@Component({
  selector: "app-home",
  template: ``,
})
export class Home implements OnInit {
  private readonly router = inject(Router)
  private readonly localStorageProvider = inject(LocalStorageProvider)

  async ngOnInit() {
    const result = await this.localStorageProvider.load<boolean>(
      Constants.Storage.BootCodeMvpUserIsOld,
    )

    if (result.isOk && result.value === true) {
      this.router.navigate(["curriculum"])
      return
    }

    this.router.navigate(["intro"])
  }
}
