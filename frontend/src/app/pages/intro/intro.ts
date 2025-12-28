import { Component, inject, type OnInit } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorRocketLaunchFill } from "@ng-icons/phosphor-icons/fill"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { LocalStorageProvider } from "#/app/common/local-storage-provider.service"
import { Constants } from "#/app/constants"

@Component({
  selector: "app-intro",
  imports: [HlmButtonImports, RouterLink, NgIcon],
  host: { class: "layout-phone justify-center text-center gap-4 p-4" },
  viewProviders: [provideIcons({ phosphorRocketLaunchFill })],
  templateUrl: "./intro.html",
})
export class Intro implements OnInit {
  private readonly localStorageProvider = inject(LocalStorageProvider)

  async ngOnInit() {
    await this.localStorageProvider.save(
      Constants.Storage.BootCodeMvpUserIsOld,
      true,
    )
  }
}
