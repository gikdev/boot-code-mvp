import { Component, inject, signal } from "@angular/core"
import { ActivatedRoute, RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import {
  phosphorCaretRight,
  phosphorNotePencil,
  phosphorPencilSimpleLine,
  phosphorSpinnerGap,
  phosphorTrash,
} from "@ng-icons/phosphor-icons/regular"
import { injectQuery } from "@tanstack/angular-query-experimental"
import { getLessonByIdOptions } from "#/api/generated/client"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { Mobile } from "#/app/layouts/mobile/mobile"
import { HlmButtonImports } from "#/libs/ui/button/src"

@Component({
  selector: "app-lesson",
  imports: [HlmButtonImports, NgIcon, Mobile, RouterLink, ShowIfAdmin],
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

  protected isPopulatedString = (value: string | null): value is string =>
    value != null && value.trim().length > 0

  protected getFileAndMimeType = (fileAndMimeType: string) => {
    const [file, mimeType] = fileAndMimeType.split(",")
    return { file, mimeType }
  }

  protected getFullFileUrl = (fileAndMimeType: string) =>
    `https://wd-bahrami.storage.iran.liara.space/boot-code/${this.getFileAndMimeType(fileAndMimeType).file}`
}

function isStringWithContent(value: unknown): value is string {
  return typeof value === "string" && value.trim().length > 0
}
