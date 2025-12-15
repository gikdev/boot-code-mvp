import { HttpClient } from "@angular/common/http"
import { Component, DestroyRef, inject, signal } from "@angular/core"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-intro",
    imports: [TestNav, HlmButtonImports],
    template: `
        <p>intro works!</p>
        <app-test-nav />
        <button hlmBtn (click)="callServer()">Hello world!</button>
        @let api = apiState();

        @switch (api.status) {
            @case ('idle') {
                <p>Idle.</p>
            }

            @case ('loading') {
                <p>Loading...</p>
            }

            @case ('error') {
                <p>Something went wrong: {{ api.message }}</p>
            }

            @case ('success') {
                <p>The server says: {{ api.data.message }}</p>
            }

            @default {
                <div>nothing...</div>
            }
        }
    `,
    styles: ``,
})
export class Intro {
    protected apiState = signal<ApiState>({ status: "idle" })
    private readonly _httpClient = inject(HttpClient)
    private readonly _destroyRef = inject(DestroyRef)

    protected callServer() {
        this.apiState.set({ status: "loading" })

        const subscription = this._httpClient.get<MessageResponse>("http://localhost:5125/").subscribe({
            next: (val: MessageResponse) => this.apiState.set({ status: "success", data: val }),
            error: err => this.apiState.set({ status: "error", message: err.message }),
        })

        this._destroyRef.onDestroy(() => subscription.unsubscribe())
    }
}

type ApiState =
    | { status: "idle" }
    | { status: "error"; message: string }
    | { status: "loading" }
    | { status: "success"; data: MessageResponse }

interface MessageResponse {
    message: string
}
