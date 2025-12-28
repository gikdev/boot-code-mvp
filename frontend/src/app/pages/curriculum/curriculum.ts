import { Component } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import {
  phosphorCaretLeft,
  phosphorNotebook,
} from "@ng-icons/phosphor-icons/regular"
import { HlmCard, HlmCardTitle } from "@spartan-ng/helm/card"
import { injectQuery } from "@tanstack/angular-query-experimental"
import { listLessonsOptions } from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { HlmButtonImports } from "#/libs/ui/button/src"

@Component({
  selector: "app-curriculum",
  imports: [HlmCard, HlmCardTitle, RouterLink, HlmButtonImports, NgIcon],
  templateUrl: "./curriculum.html",
  viewProviders: [provideIcons({ phosphorNotebook, phosphorCaretLeft })],
  host: {
    class: "layout-phone items-center justify-center gap-4 p-4",
  },
})
export class Curriculum {
  lessonsQuery = injectQuery(listLessonsOptions)

  protected getLessonUrl = (lessonId: string) => AppRoutes.lesson(lessonId)
}
