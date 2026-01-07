import type { Routes } from "@angular/router"
import { Curriculum } from "./pages/curriculum/curriculum"
import { Dev } from "./pages/dev/dev"
import { Home } from "./pages/home/home"
import { Intro } from "./pages/intro/intro"
import { EditLesson } from "./pages/lessons/edit-lesson/edit-lesson"
import { EditLessonContent } from "./pages/lessons/edit-lesson-content/edit-lesson-content"
import { LessonDetails } from "./pages/lessons/lesson-details/lesson-details"
import { NewLesson } from "./pages/lessons/new-lesson/new-lesson"

export const AppRoutes = {
  home: () => "/",
  intro: () => "/intro",
  curriculum: () => "/curriculum",
  dev: () => "/dev",
  lessons: {
    details: (lessonId: string) => `/lessons/${lessonId}`,
    create: () => `/lessons/new`,
    edit: (lessonId: string) => `/lessons/edit/${lessonId}`,
    editContent: (lessonId: string) => `/lessons/edit/${lessonId}/content`,
  },
}

export const routes: Routes = [
  { path: AppRoutes.home().slice(1), component: Home },
  { path: AppRoutes.intro().slice(1), component: Intro },
  { path: AppRoutes.curriculum().slice(1), component: Curriculum },
  { path: AppRoutes.dev().slice(1), component: Dev },
  { path: AppRoutes.lessons.create().slice(1), component: NewLesson },
  {
    path: AppRoutes.lessons.details(":lessonId").slice(1),
    component: LessonDetails,
  },
  {
    path: AppRoutes.lessons.edit(":lessonId").slice(1),
    component: EditLesson,
  },
  {
    path: AppRoutes.lessons.editContent(":lessonId").slice(1),
    component: EditLessonContent,
  },
]
