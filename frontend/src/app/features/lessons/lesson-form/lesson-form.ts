import { Component, inject } from "@angular/core"
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowRight } from "@ng-icons/phosphor-icons/regular"
import { HotToastService } from "@ngneat/hot-toast"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { HlmFieldImports } from "@spartan-ng/helm/field"
import { HlmInputImports } from "@spartan-ng/helm/input"
import { injectMutation } from "@tanstack/angular-query-experimental"
import * as v from "valibot"
import {
  createLessonMutation,
  type LessonSmallResponse,
} from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"

@Component({
  selector: "app-lesson-form",
  imports: [
    HlmButtonImports,
    HlmInputImports,
    HlmFieldImports,
    ReactiveFormsModule,
    NgIcon,
    RouterLink,
  ],
  providers: [provideIcons({ phosphorArrowRight })],
  template: `
    <form [formGroup]="lesson" (ngSubmit)="onSubmit()">
			<div hlmFieldGroup>
				<fieldset hlmFieldSet>
					<legend hlmFieldLegend>درس جدید</legend>
					<p hlmFieldDescription>یه درس جدید بساز!</p>

					<div hlmFieldGroup>
						<div hlmField>
							<label hlmFieldLabel for="lesson-title">عنوان درس</label>
							<input hlmInput placeholder="..." id="lesson-title"  formControlName="title" />
						</div>
          </div>
        </fieldset>

				<div hlmField orientation="horizontal">
          <a hlmBtn variant="outline" [routerLink]="getCurriculumPage()">
            <ng-icon name="phosphorArrowRight" />
            <span>بازگشت</span>
          </a>

					<button hlmBtn variant="default" class="flex-1" type="submit">ایجاد</button>
				</div>
			</div>
		</form>
  `,
})
export class LessonForm {
  private toast = inject(HotToastService)
  private formBuilder = inject(FormBuilder)
  protected createLessonMutation = injectMutation(createLessonMutation)

  protected lesson = this.formBuilder.group({
    title: ["", Validators.required],
  })

  protected getCurriculumPage = () => AppRoutes.curriculum()

  protected onSubmit() {
    const value = this.lesson.value

    const result = v.safeParse(LessonSchema, value)

    if (!result.success) {
      const finalMsg = result.issues.map(i => i.message).join(" - ")
      this.toast.error(finalMsg)
      return
    }

    const body = result.output

    const onSuccess = (_data: LessonSmallResponse) => {
      this.toast.success("با موفقیت انجام شد!")
      this.lesson.reset()
    }

    const onError = (error: Error) => {
      this.toast.error(`یه مشکلی پیش آمده - ${error.message}`)
      console.error(error)
    }

    this.createLessonMutation.mutate({ body }, { onSuccess, onError })
  }
}

const LessonSchema = v.object({
  title: v.pipe(
    v.string("عنوان باید از نوع رشته باشد"),
    v.nonEmpty("مقدار عنوان نباید خالی باشد"),
  ),
})
