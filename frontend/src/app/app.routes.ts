import type { Routes } from "@angular/router"
import { Curriculum } from "./pages/curriculum/curriculum"
import { Home } from "./pages/home/home"
import { Intro } from "./pages/intro/intro"
import { EditLesson } from "./pages/lessons/edit-lesson/edit-lesson"
import { LessonDetails } from "./pages/lessons/lesson-details/lesson-details"
import { NewLesson } from "./pages/lessons/new-lesson/new-lesson"

export const AppRoutes = {
  home: () => "/",
  intro: () => "/intro",
  curriculum: () => "/curriculum",
  lessons: {
    details: (lessonId: string) => `/lessons/${lessonId}`,
    create: () => `/lessons/new`,
    edit: (lessonId: string) => `/lessons/edit/${lessonId}`,
  },
}

export const routes: Routes = [
  { path: AppRoutes.home().slice(1), component: Home },
  { path: AppRoutes.intro().slice(1), component: Intro },
  { path: AppRoutes.curriculum().slice(1), component: Curriculum },
  { path: AppRoutes.lessons.create().slice(1), component: NewLesson },
  {
    path: AppRoutes.lessons.details(":lessonId").slice(1),
    component: LessonDetails,
  },
  {
    path: AppRoutes.lessons.edit(":lessonId").slice(1),
    component: EditLesson,
  },
]
