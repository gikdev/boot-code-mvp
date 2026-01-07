import { Component, inject, signal } from "@angular/core"
import { ActivatedRoute, Router, RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import {
  phosphorCaretRight,
  phosphorNotePencil,
  phosphorPencilSimpleLine,
  phosphorSpinnerGap,
  phosphorTrash,
} from "@ng-icons/phosphor-icons/regular"
import { HotToastService } from "@ngneat/hot-toast"
import {
  injectMutation,
  injectQuery,
} from "@tanstack/angular-query-experimental"
import { MarkdownComponent } from "ngx-markdown"
import {
  deleteLessonByIdMutation,
  getLessonByIdOptions,
} from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { Mobile } from "#/app/layouts/mobile/mobile"
import { HlmButtonImports } from "#/libs/ui/button/src"
import { isStringWithContent } from "#/libs/utils"

@Component({
  selector: "app-lesson",
  imports: [
    HlmButtonImports,
    NgIcon,
    Mobile,
    RouterLink,
    ShowIfAdmin,
    MarkdownComponent,
  ],
  viewProviders: [
    provideIcons({
      phosphorArrowCounterClockwiseFill,
      phosphorSpinnerGap,
      phosphorPencilSimpleLine,
      phosphorNotePencil,
      phosphorCaretRight,
      phosphorTrash,
    }),
  ],
  templateUrl: "./lesson-details.html",
})
export class LessonDetails {
  lessonId = signal<string | null>(null)
  removeLessonMutation = injectMutation(deleteLessonByIdMutation)
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
  private readonly toast = inject(HotToastService)
  private readonly router = inject(Router)

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

  protected removeLesson() {
    const lessonId = this.lessonId()
    const isSure = window.confirm("Sure?")
    if (isSure === false || typeof lessonId !== "string") return

    this.removeLessonMutation.mutate(
      {
        path: { id: lessonId },
      },
      {
        onSuccess: () => {
          this.toast.success("با موفقیت انجام شد.")
          this.router.navigate([AppRoutes.curriculum()])
        },
        onError: error => {
          this.toast.error(`یه مشکلی پیش آمد: ${error.message}`)
        },
      },
    )
  }

  protected isPopulatedString = (value: string | null): value is string =>
    typeof value === "string" && value.trim().length > 0

  protected getFileAndMimeType = (fileAndMimeType: string) => {
    const [file, mimeType] = fileAndMimeType.split(",")
    return { file, mimeType }
  }

  protected getFullFileUrl = (fileAndMimeType: string) =>
    `https://wd-bahrami.storage.iran.liara.space/boot-code/${this.getFileAndMimeType(fileAndMimeType).file}`

  protected getEditPageUrl = () => {
    const lessonId = this.lessonId()

    if (isStringWithContent(lessonId)) return AppRoutes.lessons.edit(lessonId)

    return AppRoutes.home()
  }

  protected getEditContentPageUrl = () => {
    const lessonId = this.lessonId()

    if (isStringWithContent(lessonId))
      return AppRoutes.lessons.editContent(lessonId)

    return AppRoutes.home()
  }
}
