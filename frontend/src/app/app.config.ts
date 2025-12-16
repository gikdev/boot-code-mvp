import { provideHttpClient, withFetch } from "@angular/common/http"
import { type ApplicationConfig, provideBrowserGlobalErrorListeners } from "@angular/core"
import { provideRouter } from "@angular/router"
import { client } from "@generated-api-client"
import { provideHeyApiClient } from "@generated-api-client/client/client.gen"
import { provideTanStackQuery } from "@tanstack/angular-query-experimental"
import { queryClient } from "../integrations/query-client"
import { routes } from "./app.routes"

export const appConfig: ApplicationConfig = {
    providers: [
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes),
        provideHttpClient(withFetch()),
        provideHeyApiClient(client),
        provideTanStackQuery(queryClient),
    ],
}
