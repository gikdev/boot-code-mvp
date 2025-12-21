import { defineConfig } from "@hey-api/openapi-ts"

export default defineConfig({
  input: "http://localhost:5263/openapi/v1.json",
  output: {
    path: "src/api/generated/client",
    format: "biome",
    lint: null,
  },
  plugins: [
    {
      name: "@hey-api/typescript",
      enums: "javascript",
      exportFromIndex: true,
    },
    { name: "@hey-api/sdk", exportFromIndex: true },
    { name: "@hey-api/client-angular", exportFromIndex: true },
    { name: "@tanstack/angular-query-experimental", exportFromIndex: true },
  ],
})
