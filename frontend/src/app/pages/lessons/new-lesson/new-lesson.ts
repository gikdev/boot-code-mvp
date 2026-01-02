import { Component } from "@angular/core"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { LessonForm } from "#/app/features/lessons/lesson-form/lesson-form"
import { Mobile } from "#/app/layouts/mobile/mobile"

@Component({
  selector: "app-new-lesson",
  imports: [ShowIfAdmin, Mobile, LessonForm],
  templateUrl: "./new-lesson.html",
})
export class NewLesson {}
