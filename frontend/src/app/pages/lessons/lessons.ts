import { Component, inject, signal } from "@angular/core"
import { ActivatedRoute } from "@angular/router"
import { TestNav } from "../../test-nav"

@Component({
  selector: "app-lesson",
  imports: [TestNav],
  template: `
    <app-test-nav />
    <p> Lesson works! #{{ lessonId() }} </p>
  `,
})
export class Lessons {
  lessonId = signal("")

  private readonly activatedRoute = inject(ActivatedRoute)

  constructor() {
    this.handleParams()
  }

  private handleParams() {
    this.activatedRoute.params.subscribe(params => {
      // biome-ignore lint/complexity/useLiteralKeys: I'd get TS error otherwise!
      this.lessonId.set(params["lessonId"])
    })
  }
}
