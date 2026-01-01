import { Injectable, inject, signal } from "@angular/core"
import { createAdminSession } from "@generated-api-client"
import { HotToastService } from "@ngneat/hot-toast"

@Injectable({ providedIn: "root" })
export class Auth {
  isAdmin = signal(false)
  toast = inject(HotToastService)

  async login(code: string): Promise<boolean> {
    const { data, error } = await createAdminSession({
      body: {
        passcode: code,
      },
    })

    if (error || !data) {
      this.toast.error("یه مشکلی پیش آمد.")
      console.error({ error, data })
      return false
    }

    const isAdmin = data.isAdmin

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

  logout = () => this.isAdmin.set(false)
}
