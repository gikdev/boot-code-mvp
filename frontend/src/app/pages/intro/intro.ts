import { Component } from "@angular/core"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-intro",
    imports: [TestNav],
    template: `
        <p>
        intro works!
        </p>
        <app-test-nav />
    `,
    styles: ``,
})
export class Intro {}
