import { Component, effect, inject, input, output } from "@angular/core"
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowRight } from "@ng-icons/phosphor-icons/regular"
import { HotToastService } from "@ngneat/hot-toast"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { HlmFieldImports } from "@spartan-ng/helm/field"
import { HlmInputImports } from "@spartan-ng/helm/input"
import * as v from "valibot"
import { AppRoutes } from "#/app/app.routes"
import { type LessonFormValue, LessonSchema } from "./types"

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
					<legend hlmFieldLegend>{{ this.mode() === "CREATE" ? "درس جدید" : "ویرایش درس" }}</legend>

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

					<button hlmBtn variant="default" class="flex-1" type="submit">ذخیره</button>
				</div>
			</div>
		</form>
  `,
})
export class LessonForm {
  private toast = inject(HotToastService)
  private formBuilder = inject(FormBuilder)
  public mode = input.required<"CREATE" | "EDIT">()
  public initialValue = input<LessonFormValue | null>(null)
  public submitForm = output<LessonFormValue>()

  constructor() {
    effect(() => {
      const value = this.initialValue()
      if (!value) return
      this.lesson.patchValue(value)
    })
  }

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

    this.submitForm.emit(result.output)
  }
}
