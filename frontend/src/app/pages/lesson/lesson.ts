import { Component, inject, signal } from "@angular/core"
import { ActivatedRoute } from "@angular/router"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-lesson",
    imports: [TestNav],
    template: `
        <p> Lesson works! #{{ lessonId() }} </p>
        <app-test-nav />
    `,
})
export class Lesson {
    public static params = {
        lessonId: {
            key: "lessonId",
            typeOf: null as unknown as string,
        },
    }

    lessonId = signal<typeof Lesson.params.lessonId.typeOf>("")

    private readonly activatedRoute = inject(ActivatedRoute)

    constructor() {
        this.handleParams()
    }

    private handleParams() {
        this.activatedRoute.params.subscribe(params => {
            this.lessonId.set(params[Lesson.params.lessonId.key])
        })
    }
}
