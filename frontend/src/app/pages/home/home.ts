import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Task } from "true-myth/task";

@Component({
  selector: 'app-home',
  imports: [],
  template: ``,
  styles: ``,
  providers: [
      // { provide   }
  ],
})
export class Home implements OnInit {
    private readonly router = inject(Router)

    ngOnInit(): void {
        this.router.navigate(["intro"])
    }
}

interface IStorageProvider {
    save(key: string, data: unknown): Task<void, Error>;
    load<T>(key: string): Task<T, Error>;
}

class LocalStorageProvider implements IStorageProvider {
  save = (key: string, data: unknown): Task<void, Error> =>
    new Task<void, Error>((resolve, reject) => {
      try {
        const serialized = JSON.stringify(data)
        localStorage.setItem(key, serialized)
        resolve(undefined)
      } catch (err) {
        reject(err instanceof Error ? err : new Error('Failed to save to localStorage'))
      }
    })

  load = <T>(key: string): Task<T, Error> =>
    new Task<T, Error>((resolve, reject) => {
      try {
        const raw = localStorage.getItem(key)

        if (raw === null) {
          reject(new Error(`Key "${key}" not found in localStorage`))
          return
        }

        const parsed = JSON.parse(raw) as T
        resolve(parsed)
      } catch (err) {
        reject(err instanceof Error ? err : new Error('Failed to load from localStorage'))
      }
    })
}

