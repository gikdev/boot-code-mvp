import { Component, inject, type OnInit } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorRocketLaunchFill } from "@ng-icons/phosphor-icons/fill"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { Constants } from "#/app/constants"
import { OfflineStorageProvider } from "#/app/features/storage/offline-storage-provider"
import { Mobile } from "#/app/layouts/mobile/mobile"

@Component({
  selector: "app-intro",
  imports: [HlmButtonImports, RouterLink, NgIcon, Mobile],
  viewProviders: [provideIcons({ phosphorRocketLaunchFill })],
  templateUrl: "./intro.html",
})
export class Intro implements OnInit {
  private readonly offlineStorage = inject(OfflineStorageProvider)

  ngOnInit() {
    this.offlineStorage.save(Constants.Storage.UserIsOld, true)
  }
}
