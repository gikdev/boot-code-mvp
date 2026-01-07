import {
  CdkDrag,
  type CdkDragDrop,
  CdkDropList,
  moveItemInArray,
} from "@angular/cdk/drag-drop"
import { Component, inject, signal } from "@angular/core"
import { RouterLink } from "@angular/router"
import { NgIcon, provideIcons } from "@ng-icons/core"
import { phosphorArrowCounterClockwiseFill } from "@ng-icons/phosphor-icons/fill"
import {
  phosphorCheck,
  phosphorDotsSixVertical,
  phosphorHandGrabbing,
  phosphorPlus,
  phosphorProhibit,
  phosphorSpinnerGap,
} from "@ng-icons/phosphor-icons/regular"
import { HotToastService } from "@ngneat/hot-toast"
import {
  injectMutation,
  injectQuery,
} from "@tanstack/angular-query-experimental"
import {
  type ChangeLessonsPositionsRequest,
  changeLessonsPositionsMutation,
  listLessonsOptions,
} from "#/api/generated/client"
import { AppRoutes } from "#/app/app.routes"
import { ShowIfAdmin } from "#/app/features/auth/show-if-admin/show-if-admin"
import { LessonCard } from "#/app/features/lessons/lesson-card/lesson-card"
import { Mobile } from "#/app/layouts/mobile/mobile"
import { HlmButtonImports } from "#/libs/ui/button/src"

@Component({
  selector: "app-curriculum",
  imports: [
    HlmButtonImports,
    NgIcon,
    LessonCard,
    RouterLink,
    ShowIfAdmin,
    Mobile,
    CdkDropList,
    CdkDrag,
  ],
  templateUrl: "./curriculum.html",
  viewProviders: [
    provideIcons({
      phosphorArrowCounterClockwiseFill,
      phosphorSpinnerGap,
      phosphorPlus,
      phosphorDotsSixVertical,
      phosphorHandGrabbing,
      phosphorCheck,
      phosphorProhibit,
    }),
  ],
})
export class Curriculum {
  private readonly toast = inject(HotToastService)
  protected sorting = signal<SortingState>({ state: "IDLE" })
  protected lessonsQuery = injectQuery(listLessonsOptions)
  protected getNewLessonPage = () => AppRoutes.lessons.create()
  protected changePositionsMutation = injectMutation(
    changeLessonsPositionsMutation,
  )

  protected getLessonUrl = (lessonId: string) =>
    AppRoutes.lessons.details(lessonId)

  protected startSorting = () => {
    if (this.sorting().state === "IS_BEING_SORTED") return

    const data = this.lessonsQuery.data()
    if (!data) return

    const items = data.items.map(
      (lesson, index) =>
        ({
          id: lesson.id,
          title: lesson.title,
          position: index + 1,
        }) satisfies Position,
    )

    this.sorting.set({ state: "IS_BEING_SORTED", items })
  }

  protected endSorting() {
    if (this.sorting().state === "IDLE") return
    this.sorting.set({ state: "IDLE" })
  }

  protected saveSorting() {
    const sorting = this.sorting()
    if (sorting.state === "IDLE") return

    const body: ChangeLessonsPositionsRequest = {
      lessons: sorting.items.map(i => ({
        lessonId: i.id,
        newPosition: i.position,
      })),
    }

    this.changePositionsMutation.mutate(
      { body },
      {
        onError: error =>
          this.toast.error(`یه مشکلی پیش اومد: ${error.message}`),
        onSuccess: async (_data, _variables, _onMutateResult, context) => {
          await context.client.invalidateQueries(listLessonsOptions())
          this.toast.success("با موفقیت انجام شد")
          this.endSorting()
        },
      },
    )
  }

  protected drop(event: CdkDragDrop<string[]>) {
    const sorting = this.sorting()
    if (sorting.state !== "IS_BEING_SORTED") return

    moveItemInArray(sorting.items, event.previousIndex, event.currentIndex)
    this.updateItemPositions()
  }

  private updateItemPositions() {
    const sorting = this.sorting()
    if (sorting.state !== "IS_BEING_SORTED") return

    sorting.items.forEach((item, index) => {
      item.position = index + 1
    })

    this.sorting.set({ state: "IS_BEING_SORTED", items: [...sorting.items] })
  }
}

type SortingState =
  | { state: "IS_BEING_SORTED"; items: Position[] }
  | { state: "IDLE" }

type Position = {
  id: string
  title: string
  position: number
}
