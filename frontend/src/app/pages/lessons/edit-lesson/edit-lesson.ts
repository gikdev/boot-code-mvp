import { Component, inject, signal } from "@angular/core"
import { ActivatedRoute, Router } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import { phosphorSpinnerGap } from "@ng-icons/phosphor-icons/regular"
import { HotToastService } from "@ngneat/hot-toast"
import {
  injectMutation,
  injectQuery,
} from "@tanstack/angular-query-experimental"
import {
  getLessonByIdOptions,
  updateLessonItselfByIdMutation,
} from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { LessonForm } from "#/app/features/lessons/lesson-form/lesson-form"
import type { LessonFormValue } from "#/app/features/lessons/lesson-form/types"
import { Mobile } from "#/app/layouts/mobile/mobile"
import { isStringWithContent } from "#/libs/utils"

@Component({
  selector: "app-edit-lesson",
  templateUrl: "./edit-lesson.html",
  imports: [LessonForm, NgIcon, Mobile, ShowIfAdmin],
  viewProviders: [
    provideIcons({
      phosphorArrowCounterClockwiseFill,
      phosphorSpinnerGap,
    }),
  ],
})
export class EditLesson {
  private readonly router = inject(Router)
  private readonly toast = inject(HotToastService)
  private readonly activatedRoute = inject(ActivatedRoute)
  private readonly updateLessonMutation = injectMutation(
    updateLessonItselfByIdMutation,
  )

  protected lessonId = signal<string | null>(null)
  protected lessonQuery = injectQuery(() => ({
    ...getLessonByIdOptions({
      path: {
        // biome-ignore lint/style/noNonNullAssertion: handled by Tanstack Query.
        id: this.lessonId()!,
      },
    }),
    enabled: () => isStringWithContent(this.lessonId()),
  }))

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
  protected updateLesson(body: LessonFormValue) {
    const id = this.lessonId()
    if (!isStringWithContent(id)) return

    this.updateLessonMutation.mutate(
      { path: { id }, body },
      {
        onError: e => this.toast.error(e.message),
        onSuccess: () => {
          this.toast.success("ویرایش شد")
          this.router.navigate([AppRoutes.lessons.details(id)])
        },
      },
    )
  }
}
