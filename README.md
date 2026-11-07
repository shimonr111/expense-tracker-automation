# Expense Tracker UI Automation

This is a separate C# Playwright test project built using the Page Object Model (POM) pattern.

## Structure

- `Pages/` - page objects and shared page behaviors
- `Tests/` - smoke tests and future end-to-end scenarios
- `PlaywrightSettings.cs` - base URL configuration

## Run locally

From this folder:

```bash
dotnet test
```

You can override the application under test with an environment variable:

```bash
set EXPENSE_TRACKER_BASE_URL=https://shimonr111.github.io/expense-tracker
dotnet test
```

## Notes

- The project targets the public expense tracker app by default.
- The starter tests validate that the login page loads and shows the expected auth prompt.
- Add new page objects and tests in the same pattern to expand coverage.
