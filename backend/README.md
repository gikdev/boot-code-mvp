# Backend

- NOTE: for migrations and stuff:
    - `-p` → Project where your DbContext lives.
    - `-s` → Startup project, i.e., the project that has the configuration to actually run the app (usually the one with Program.cs).

```sh
dotnet ef migrations add <NAME> -p <PROJECT> -s <STARTUP_PROJECT>
```
