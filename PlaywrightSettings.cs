namespace ExpenseTracker.Automation;

public static class PlaywrightSettings
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("EXPENSE_TRACKER_BASE_URL") ?? "https://shimonr111.github.io/expense-tracker";
}
