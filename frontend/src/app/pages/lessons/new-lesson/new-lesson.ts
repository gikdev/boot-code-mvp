import { Component, inject } from "@angular/core"
import { Router } from "@angular/router"
import { HotToastService } from "@ngneat/hot-toast"
import { injectMutation } from "@tanstack/angular-query-experimental"
import { createLessonMutation } from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { LessonForm } from "#/app/features/lessons/lesson-form/lesson-form"
import type { LessonFormValue } from "#/app/features/lessons/lesson-form/types"
import { Mobile } from "#/app/layouts/mobile/mobile"

@Component({
  selector: "app-new-lesson",
  imports: [ShowIfAdmin, Mobile, LessonForm],
  templateUrl: "./new-lesson.html",
})
export class NewLesson {
  private readonly router = inject(Router)
  private readonly toast = inject(HotToastService)
  private readonly createLessonMutation = injectMutation(createLessonMutation)

  createLesson(body: LessonFormValue) {
    this.createLessonMutation.mutate(
      { body: body },
      {
        onSuccess: () => {
          this.toast.success("درس ساخته شد")
          this.router.navigate([AppRoutes.curriculum()])
        },
        onError: e => this.toast.error(e.message),
      },
    )
  }
}
