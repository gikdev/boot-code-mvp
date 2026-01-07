import { Component, effect, inject, input, output } from "@angular/core"
import { FormBuilder, ReactiveFormsModule } from "@angular/forms"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowRight } from "@ng-icons/phosphor-icons/regular"
import { HotToastService } from "@ngneat/hot-toast"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { HlmFieldImports } from "@spartan-ng/helm/field"
import { HlmInputImports } from "@spartan-ng/helm/input"
import * as v from "valibot"
import { HlmTextarea, HlmTextareaImports } from "#/libs/ui/textarea/src"
import {
  type UpdateLessonContentFormValue,
  UpdateLessonContentSchema,
} from "./types"

@Component({
  selector: "app-lesson-content-form",
  templateUrl: "./lesson-content-form.html",
  providers: [provideIcons({ phosphorArrowRight })],
  imports: [
    HlmButtonImports,
    HlmInputImports,
    HlmTextareaImports,
    HlmFieldImports,
    ReactiveFormsModule,
    NgIcon,
    RouterLink,
    HlmTextarea,
  ],
})
export class LessonContentForm {
  private toast = inject(HotToastService)
  private formBuilder = inject(FormBuilder)
  public goBackPath = input.required<string>()
  public initialValue = input<UpdateLessonContentFormValue | null>(null)
  public submitForm = output<UpdateLessonContentFormValue>()

  constructor() {
    effect(() => {
      const value = this.initialValue()
      if (!value) return
      this.lessonContent.patchValue(value)
    })
  }

  protected lessonContent =
    this.formBuilder.group<UpdateLessonContentFormValue>({
      textContent: null,
      audioUrl: null,
      imageUrl: null,
      videoUrl: null,
    })

  protected onSubmit() {
    const value = this.lessonContent.value

    const result = v.safeParse(UpdateLessonContentSchema, value)

    if (!result.success) {
      const finalMsg = result.issues.map(i => i.message).join(" - ")
      this.toast.error(finalMsg)
      return
    }

    this.submitForm.emit(result.output)
  }
}
