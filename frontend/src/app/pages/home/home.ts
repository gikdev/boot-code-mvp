import { Component, inject, type OnInit } from "@angular/core"
import { Router } from "@angular/router"
import { LocalStorageProvider } from "#/app/common/local-storage-provider.service"

@Component({
  selector: "app-home",
  template: ``,
})
export class Home implements OnInit {
  private readonly router = inject(Router)
  private readonly localStorageProvider = inject(LocalStorageProvider)

  async ngOnInit() {
    var result = await this.localStorageProvider.load<boolean>(
      "BootCodeMvp.User.IsOld",
    )

    if (result.isOk && result.value) {
      this.router.navigate(["curriculum"])
      return
    }

    this.router.navigate(["intro"])
  }
}
