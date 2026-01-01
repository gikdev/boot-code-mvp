import { Component, input } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import {
  phosphorCaretLeft,
  phosphorNotebook,
} from "@ng-icons/phosphor-icons/regular"
import type { LessonSmallResponse } from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { HlmCardImports } from "#/libs/ui/card/src"

@Component({
  selector: "app-lesson-card",
  imports: [HlmCardImports, RouterLink, NgIcon],
  viewProviders: [provideIcons({ phosphorNotebook, phosphorCaretLeft })],
  template: `
    <a
      hlmCard
      [routerLink]="getLessonUrl(lesson().id)"
      class="flex items-center gap-2 p-4 cursor-pointer hover:bg-accent flex-row w-full"
    >
      <ng-icon name="phosphorNotebook" size="24" />
      <h3 hlmCardTitle class="flex-1">{{ lesson().title }}</h3>

      <ng-icon name="phosphorCaretLeft" size="24" />
    </a>
  `,
})
export class LessonCard {
  lesson = input.required<LessonSmallResponse>()

  protected getLessonUrl = (lessonId: string) =>
    AppRoutes.lessons.details(lessonId)
}
