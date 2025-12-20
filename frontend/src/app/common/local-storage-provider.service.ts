import { Injectable } from "@angular/core"
import { Task } from "true-myth/task"
import type { IStorageProvider } from "./storage-provider.interface"

@Injectable({
    providedIn: "root",
})
export class LocalStorageProvider implements IStorageProvider {
    save = (key: string, data: unknown): Task<void, Error> =>
        new Task<void, Error>((resolve, reject) => {
            try {
                const serialized = JSON.stringify(data)
                localStorage.setItem(key, serialized)
                resolve(undefined)
            } catch (err) {
                reject(err instanceof Error ? err : new Error("Failed to save to localStorage"))
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
                reject(err instanceof Error ? err : new Error("Failed to load from localStorage"))
            }
        })
}
