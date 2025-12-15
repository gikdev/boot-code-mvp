# Notes

- Annotate the enums used in APIs with `[JsonConverter(typeof(JsonStringEnumConverter))]` so that they will show properly in the OpenAPI definition and also the generated TypeScript code from the OpenAPI...

```csharp
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum State {
  Development,
  Production,
}
```
