import { bootstrapApplication } from "@angular/platform-browser"
import { App } from "./app/app"
import { appConfig } from "./app/app.config"
import "@phosphor-icons/web/regular"
import "@phosphor-icons/web/fill"

bootstrapApplication(App, appConfig).catch(console.error)
