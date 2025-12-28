import { Component, inject, signal } from "@angular/core"
import { ActivatedRoute } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import { phosphorSpinnerGap } from "@ng-icons/phosphor-icons/regular"
import { injectQuery } from "@tanstack/angular-query-experimental"
import { getLessonByIdOptions } from "#/api/generated/client"
import { HlmButtonImports } from "#/libs/ui/button/src"

@Component({
  selector: "app-lesson",
  imports: [HlmButtonImports, NgIcon],
  viewProviders: [
    provideIcons({ phosphorArrowCounterClockwiseFill, phosphorSpinnerGap }),
  ],
  templateUrl: "./lessons.html",
})
export class Lessons {
  lessonId = signal<string | null>(null)
  lessonQuery = injectQuery(() => ({
    ...getLessonByIdOptions({
      path: {
        // biome-ignore lint/style/noNonNullAssertion: handled by Tanstack Query.
        id: this.lessonId()!,
      },
    }),
    enabled: () => isStringWithContent(this.lessonId()),
  }))

  private readonly activatedRoute = inject(ActivatedRoute)

  constructor() {
    this.handleParams()
  }

  private handleParams() {
    this.activatedRoute.params.subscribe(params => {
      // biome-ignore lint/complexity/useLiteralKeys: I'd get TS error otherwise!
      const lessonId = params["lessonId"]
      if (!isStringWithContent(lessonId)) return
      this.lessonId.set(lessonId)
    })
  }
}

function isStringWithContent(value: unknown): value is string {
  return typeof value === "string" && value.trim().length > 0
}
