import { Component, inject, type OnInit } from "@angular/core"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { LocalStorageProvider } from "#/app/common/local-storage-provider.service"
import { Mobile } from "#/app/layouts/mobile/mobile"
import "@phosphor-icons/web/regular"
import "@phosphor-icons/web/fill"
import { RouterLink } from "@angular/router";
import { Constants } from "#/app/constants"

@Component({
  selector: "app-intro",
  imports: [HlmButtonImports, Mobile, RouterLink],
  template: `
    <app-layout-mobile class="justify-center text-center gap-4">
      <h1 class="text-3xl font-black">بوت‌کد؛ <br /> بوت‌کمپ 👨🏻‍💻 کدنویسی</h1>

      <img src="/in-the-zone.svg" alt="" class="max-w-full">

      <p>اینجا قراره به دنیای برنامه‌نویسی پا بذاریم! </p>

      <a hlmBtn variant="default" routerLink="/">
        <span>شروع کن!</span>
        <i class="ph-fill ph-rocket-launch text-lg"></i>
      </a>
    </app-layout-mobile>
  `,
})
export class Intro implements OnInit {
  private readonly localStorageProvider = inject(LocalStorageProvider)

  async ngOnInit() {
    await this.localStorageProvider.save(Constants.Storage.BootCodeMvpUserIsOld, true)
  }
}
