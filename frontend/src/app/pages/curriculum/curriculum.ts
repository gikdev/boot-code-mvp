import { Component } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import {
  phosphorPlus,
  phosphorSpinnerGap,
} from "@ng-icons/phosphor-icons/regular"
import { injectQuery } from "@tanstack/angular-query-experimental"
import { listLessonsOptions } from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { LessonCard } from "#/app/features/lessons/lesson-card/lesson-card"
import { Mobile } from "#/app/layouts/mobile/mobile"
import { HlmButtonImports } from "#/libs/ui/button/src"

@Component({
  selector: "app-curriculum",
  imports: [
    HlmButtonImports,
    NgIcon,
    LessonCard,
    RouterLink,
    ShowIfAdmin,
    Mobile,
  ],
  templateUrl: "./curriculum.html",
  viewProviders: [
    provideIcons({
      phosphorArrowCounterClockwiseFill,
      phosphorSpinnerGap,
      phosphorPlus,
    }),
  ],
})
export class Curriculum {
  protected lessonsQuery = injectQuery(listLessonsOptions)
  protected getNewLessonPage = () => AppRoutes.lessons.create()

  protected getLessonUrl = (lessonId: string) =>
    AppRoutes.lessons.details(lessonId)
}
