import { Component } from "@angular/core"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-curriculum",
    imports: [TestNav],
    template: `
        <app-test-nav />
        <p>curriculum works!</p>
    `,
})
export class Curriculum {}
