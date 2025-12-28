import { Component } from "@angular/core"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import { phosphorSpinnerGap } from "@ng-icons/phosphor-icons/regular"
import { injectQuery } from "@tanstack/angular-query-experimental"
import { listLessonsOptions } from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { LessonCard } from "#/app/features/lessons/lesson-card/lesson-card"
import { HlmButtonImports } from "#/libs/ui/button/src"

@Component({
  selector: "app-curriculum",
  imports: [HlmButtonImports, NgIcon, LessonCard],
  templateUrl: "./curriculum.html",
  viewProviders: [
    provideIcons({ phosphorArrowCounterClockwiseFill, phosphorSpinnerGap }),
  ],
  host: {
    class: "layout-phone items-center justify-center gap-4 p-4",
  },
})
export class Curriculum {
  lessonsQuery = injectQuery(listLessonsOptions)

  protected getLessonUrl = (lessonId: string) => AppRoutes.lesson(lessonId)
}
