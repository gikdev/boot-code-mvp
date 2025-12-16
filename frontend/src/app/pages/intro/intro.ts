import { Component, inject } from "@angular/core"
import { createLessonMutation, listLessonsOptions } from "@generated-api-client"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { injectMutation, injectQuery, QueryClient } from "@tanstack/angular-query-experimental"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-intro",
    imports: [TestNav, HlmButtonImports],
    template: `
        <p>intro works!</p>
        <app-test-nav />
        <button hlmBtn (click)="createLesson()">Hello world!</button>

        @switch (listLessonsQuery.status()) {
            @case ('pending') {
                <p>Loading...</p>
            }

            @case ('error') {
                <p>Something went wrong: {{ listLessonsQuery.error()?.message }}</p>
            }

            @case ('success') {
                <p>Lessons:</p>
                @for (lesson of listLessonsQuery.data()!.items; track lesson.id) {
                    <p>{{lesson.position}} - {{lesson.title}}</p>
                }
            }

            @default {
                <div>nothing...</div>
            }
        }
    `,
})
export class Intro {
    queryClient = inject(QueryClient)
    listLessonsQuery = injectQuery(listLessonsOptions)
    createLessonMutation = injectMutation(() => ({
        ...createLessonMutation(),
        onSuccess: () => {
            this.queryClient.invalidateQueries(listLessonsOptions())
        },
    }))

    createLesson() {
        this.createLessonMutation.mutate({
            body: {
                title: "یه درس بی‌ربط",
                position: 2,
            },
        })
    }
}
