import { Component } from "@angular/core"
import { RouterLink } from "@angular/router"

@Component({
    selector: "app-test-nav",
    imports: [RouterLink],
    template: `
        <nav>
            <a routerLink="/intro">Intro</a>
            <span> | </span>
            <a routerLink="/curriculum">Curriculum</a>
            <span> | </span>
            <a routerLink="/lesson/pashmak">Lesson 'pashmak'</a>
        </nav>
    `,
})
export class TestNav {}
