import type { Routes } from "@angular/router"
import { Curriculum } from "./pages/curriculum/curriculum"
import { Home } from "./pages/home/home"
import { Intro } from "./pages/intro/intro"
import { Lesson } from "./pages/lesson/lesson"

export const routes: Routes = [
    { path: "", component: Home },
    { path: "intro", component: Intro },
    { path: "curriculum", component: Curriculum },
    { path: `lesson/:${Lesson.params.lessonId.key}`, component: Lesson },
]
