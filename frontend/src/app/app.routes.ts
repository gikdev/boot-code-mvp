import type { Routes } from "@angular/router"
import { Curriculum } from "./pages/curriculum/curriculum"
import { Intro } from "./pages/intro/intro"
import { Lesson } from "./pages/lesson/lesson"

export const routes: Routes = [
    { path: "", component: Intro },
    { path: "curriculum", component: Curriculum },
    { path: `lesson/:${Lesson.params.lessonId.key}`, component: Lesson },
]
