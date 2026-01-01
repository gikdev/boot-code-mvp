import { Component } from "@angular/core"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin";

@Component({
  selector: "app-new-lesson",
  imports: [ShowIfAdmin],
  templateUrl: "./new-lesson.html",
})
export class NewLesson {}
