import { Injectable, inject, signal } from "@angular/core"
import { createAdminSession } from "@generated-api-client"
import { HotToastService } from "@ngneat/hot-toast"
import { Constants } from "#/app/constants"
import { OfflineStorageProvider } from "../storage/offline-storage-provider"

@Injectable({ providedIn: "root" })
export class Auth {
  private storage = inject(OfflineStorageProvider)
  private toast = inject(HotToastService)
  public isAdmin = signal(
    this.storage
      .load<boolean>(Constants.Storage.IsUserAdmin, true)
      .unwrapOr(false),
  )

  async login(code: string): Promise<boolean> {
    const { data, error } = await createAdminSession({
      body: {
        passcode: code,
      },
    })

    if (error || !data) {
      this.toast.error("یه مشکل در ارتباط با سرور پیش آمد.")
      console.error({ error, data })
      return false
    }

    const isAdmin = data.isAdmin

    const result = this.storage.save(
      Constants.Storage.IsUserAdmin,
      isAdmin,
      true,
    )

    if (!result.isOk) {
      this.toast.error("یه مشکلی در ذخیره‌سازی پیش آمد.")
      console.error(result.error)
      return false
    }

    this.isAdmin.set(isAdmin)

    return isAdmin
  }

  askForLogin() {
    const passcode = window.prompt("PASSCODE:")

    if (!passcode) {
      this.toast.info("کنسل شد، چون مقداری وارد نشد.")
      return
    }

    this.login(passcode)
      .then(isAdmin => {
        if (isAdmin) this.toast.success("دسترسی شما فعال شد.")
        else this.toast.error("شما دسترسی ندارید!")
      })
      .catch(err => {
        this.toast.error("یه مشکلی پیش آمد.")
        console.error(err)
      })
  }

  logout = () => {
    this.isAdmin.set(false)

    const result = this.storage.save(Constants.Storage.IsUserAdmin, false, true)

    if (!result.isOk) {
      this.toast.error("یه مشکلی در ذخیره‌سازی پیش آمد.")
      console.error(result.error)
    }
  }
}
