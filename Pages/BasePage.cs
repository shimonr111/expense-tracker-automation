using Microsoft.Playwright;

namespace ExpenseTracker.Automation.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAsync(string path = "/")
    {
        var url = PlaywrightSettings.BaseUrl.TrimEnd('/');
        await Page.GotoAsync($"{url}{path}");
    }
}
