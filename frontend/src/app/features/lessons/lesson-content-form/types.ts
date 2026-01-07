import * as v from "valibot"

export const UpdateLessonContentSchema = v.object({
  textContent: v.nullable(v.string("متن باید از نوع رشته باشد")),
  audioUrl: v.nullable(v.string("آدرس فایل صوتی باید از نوع رشته باشد")),
  imageUrl: v.nullable(v.string("آدرس تصویر باید از نوع رشته باشد")),
  videoUrl: v.nullable(v.string("آدرس ویدیو باید از نوع رشته باشد")),
  // resources: v.nullable(v.array(ResourceSchema)) // TODO later
})

export type UpdateLessonContentFormValue = v.InferInput<
  typeof UpdateLessonContentSchema
>
