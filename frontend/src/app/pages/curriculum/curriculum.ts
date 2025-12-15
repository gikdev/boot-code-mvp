import { Component } from "@angular/core"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-curriculum",
    imports: [TestNav],
    template: `
        <p>
        curriculum works!
        </p>
        <app-test-nav />
    `,
})
export class Curriculum {}
