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
  type UpdateLessonContentByIdRequest,
  updateLessonContentByIdMutation,
} from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { LessonContentForm } from "#/app/features/lessons/lesson-content-form/lesson-content-form"
import type { UpdateLessonContentFormValue } from "#/app/features/lessons/lesson-content-form/types"
import { Mobile } from "#/app/layouts/mobile/mobile"
import { isStringWithContent } from "#/libs/utils"

@Component({
  selector: "app-edit-lesson-content",
  templateUrl: "./edit-lesson-content.html",
  imports: [LessonContentForm, NgIcon, Mobile, ShowIfAdmin],
  viewProviders: [
    provideIcons({
      phosphorArrowCounterClockwiseFill,
      phosphorSpinnerGap,
    }),
  ],
})
export class EditLessonContent {
  private readonly router = inject(Router)
  private readonly toast = inject(HotToastService)
  private readonly activatedRoute = inject(ActivatedRoute)
  private readonly updateLessonMutation = injectMutation(
    updateLessonContentByIdMutation,
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

  protected getGoBackPath() {
    const id = this.lessonId()
    if (!isStringWithContent(id)) return AppRoutes.home()
    return AppRoutes.lessons.details(id)
  }

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
  protected updateLessonContent(value: UpdateLessonContentFormValue) {
    const id = this.lessonId()
    if (!isStringWithContent(id)) return

    const path = { id }
    const body: UpdateLessonContentByIdRequest = {
      ...value,
      resources: null,
    }

    this.updateLessonMutation.mutate(
      { path, body },
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
