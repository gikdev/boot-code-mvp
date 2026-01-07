import * as v from "valibot"

export const LessonSchema = v.object({
  title: v.pipe(
    v.string("عنوان باید از نوع رشته باشد"),
    v.nonEmpty("مقدار عنوان نباید خالی باشد"),
  ),
})
export type LessonFormValue = v.InferInput<typeof LessonSchema>
