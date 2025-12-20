import { Component, inject, type OnInit } from "@angular/core"
import { createLessonMutation, listLessonsOptions } from "@generated-api-client"
import { HlmButtonImports } from "@spartan-ng/helm/button"
import { injectMutation, injectQuery, QueryClient } from "@tanstack/angular-query-experimental"
import { LocalStorageProvider } from "#/app/common/local-storage-provider.service"
import { TestNav } from "../../test-nav"

@Component({
    selector: "app-intro",
    imports: [TestNav, HlmButtonImports],
    template: `
        <app-test-nav />

        <p>intro works!</p>

        <button hlmBtn (click)="createLesson()">Hello world!</button>

        @switch (listLessonsQuery.status()) {
            @case ('pending') { <p>Loading...</p> }

            @case ('error') {
                <p>Something went wrong: {{ listLessonsQuery.error()?.message }}</p>
            }

            @case ('success') {
                <p>Lessons:</p>
                @for (lesson of listLessonsQuery.data()!.items; track lesson.id) {
                    <p>{{lesson.position}} - {{lesson.title}}</p>
                }
            }

            @default { <div>nothing...</div> }
        }
    `,
})
export class Intro implements OnInit {
    private readonly queryClient = inject(QueryClient)
    protected readonly listLessonsQuery = injectQuery(listLessonsOptions)
    private readonly localStorageProvider = inject(LocalStorageProvider)
    private readonly createLessonMutation = injectMutation(() => ({
        ...createLessonMutation(),
        onSuccess: () => this.queryClient.invalidateQueries(listLessonsOptions()),
    }))

    async ngOnInit() {
        await this.localStorageProvider.save("BooCodeMvp.User.IsOld", true)
    }

    createLesson() {
        this.createLessonMutation.mutate({
            body: {
                title: "یه درس بی‌ربط",
                position: 2,
            },
        })
    }
}
