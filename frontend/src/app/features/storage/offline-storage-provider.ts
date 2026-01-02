import { Injectable } from "@angular/core"
import { err, ok, type Result } from "true-myth/result"
import type { IStorageProvider } from "./storage-provider"

@Injectable({ providedIn: "root" })
export class OfflineStorageProvider implements IStorageProvider {
  save = (
    key: string,
    data: unknown,
    isSession = false,
  ): Result<void, Error> => {
    const storage = isSession ? sessionStorage : localStorage

    try {
      const serialized = JSON.stringify(data)
      storage.setItem(key, serialized)
      return ok(undefined)
    } catch (e) {
      const error =
        e instanceof Error ? e : new Error("Failed to save to storage")

      return err(error)
    }
  }

  load = <T>(key: string, isSession = false): Result<T, Error> => {
    const storage = isSession ? sessionStorage : localStorage

    try {
      const raw = storage.getItem(key)

      if (raw === null)
        return err(new Error(`Key "${key}" not found in storage`))

      const parsed = JSON.parse(raw) as T
      return ok(parsed)
    } catch (e) {
      const error =
        e instanceof Error ? e : new Error("Failed to load from storage")

      return err(error)
    }
  }
}
