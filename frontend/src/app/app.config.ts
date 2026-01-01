import { provideHttpClient, withFetch } from "@angular/common/http"
import {
  type ApplicationConfig,
  provideBrowserGlobalErrorListeners,
} from "@angular/core"
import { provideRouter } from "@angular/router"
import { client } from "@generated-api-client"
import { provideHeyApiClient } from "@generated-api-client/client/client.gen"
import { provideHotToastConfig } from "@ngneat/hot-toast"
import {
  provideTanStackQuery,
  QueryClient,
} from "@tanstack/angular-query-experimental"
import { withDevtools } from "@tanstack/angular-query-experimental/devtools"
import { routes } from "./app.routes"

const queryClient = new QueryClient()

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withFetch()),
    provideHeyApiClient(client),
    provideTanStackQuery(queryClient, withDevtools()),
    provideHotToastConfig(),
  ],
}
