import type { Result } from "true-myth/result"

export interface IStorageProvider {
  save(key: string, data: unknown): Result<void, Error>
  load<T>(key: string): Result<T, Error>
}
