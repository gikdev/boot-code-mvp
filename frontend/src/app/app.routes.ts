import type { Routes } from "@angular/router"
import { Curriculum } from "./pages/curriculum/curriculum"
import { Home } from "./pages/home/home"
import { Intro } from "./pages/intro/intro"
import { Lessons } from "./pages/lessons/lessons"

export const AppRoutes = {
  home: () => "/",
  intro: () => "/intro",
  curriculum: () => "/curriculum",
  lesson: (lessonId: string) => `/lessons/${lessonId}`,
}

export const routes: Routes = [
  { path: AppRoutes.home().slice(1), component: Home },
  { path: AppRoutes.intro().slice(1), component: Intro },
  { path: AppRoutes.curriculum().slice(1), component: Curriculum },
  { path: AppRoutes.lesson(":lessonId").slice(1), component: Lessons },
]
