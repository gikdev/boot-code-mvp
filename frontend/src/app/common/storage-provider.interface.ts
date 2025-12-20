import type { Task } from "true-myth/task"

export interface IStorageProvider {
    save(key: string, data: unknown): Task<void, Error>
    load<T>(key: string): Task<T, Error>
}
