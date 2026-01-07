import {
  CdkDrag,
  type CdkDragDrop,
  CdkDragHandle,
  CdkDropList,
  moveItemInArray,
} from "@angular/cdk/drag-drop"
import { Component } from "@angular/core"

@Component({
  selector: "app-dev",
  imports: [CdkDropList, CdkDrag, CdkDragHandle],
  template: `
    <div cdkDropList (cdkDropListDropped)="drop($event)">
      @for (movie of movies; track movie) {
        <div cdkDrag>

        <p cdkDragHandle>...</p>
        <p>{{ movie }}</p>

        </div>
      }
    </div>
  `,
})
export class Dev {
  movies = [
    "Episode I - The Phantom Menace",
    "Episode II - Attack of the Clones",
    "Episode III - Revenge of the Sith",
    "Episode IV - A New Hope",
    "Episode V - The Empire Strikes Back",
    "Episode VI - Return of the Jedi",
    "Episode VII - The Force Awakens",
    "Episode VIII - The Last Jedi",
    "Episode IX - The Rise of Skywalker",
  ]
  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.movies, event.previousIndex, event.currentIndex)
  }
}
