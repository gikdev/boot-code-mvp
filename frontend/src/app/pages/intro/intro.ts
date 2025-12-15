import { Component } from "@angular/core"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-intro",
    imports: [TestNav, HlmButtonImports],
    template: `
        <p>intro works!</p>
        <app-test-nav />
        <button hlmBtn>Hello world!</button>
    `,
    styles: ``,
})
export class Intro {}
