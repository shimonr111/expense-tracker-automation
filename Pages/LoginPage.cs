using Microsoft.Playwright;

namespace ExpenseTracker.Automation.Pages;

public class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page)
    {
    }

    // Locators
    public ILocator WelcomeTitle => Page.Locator("h2");

    public ILocator LoginMessage => Page.Locator("p");

    public ILocator LoginButton => Page.Locator("button.login-button");

    public ILocator VersionLabel => Page.Locator("#version-label");


    // Wait until login page is loaded
    public async Task WaitUntilLoadedAsync()
    {
        await WelcomeTitle.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible
        });

        await LoginButton.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible
        });

        await VersionLabel.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible
        });
    }


    // Click login button
    public async Task ClickLoginAsync()
    {
        await LoginButton.ClickAsync();
    }


    // Open Google authentication popup
    public async Task<IPage> OpenGoogleAuthenticationPopupAsync()
    {
        var popup = await Page.RunAndWaitForPopupAsync(async () =>
        {
            await LoginButton.ClickAsync();
        });

        // Wait until Google authentication page is opened
        await popup.WaitForURLAsync(
            url => url.Contains("accounts.google.com"),
            new()
            {
                Timeout = 30000,
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

        return popup;
    }

    public async Task LoginWithGoogleAsync(string email, string password)
    {
        var popup = await OpenGoogleAuthenticationPopupAsync();

        // Enter Google email
        var emailField = popup.Locator("#identifierId");

        await emailField.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });

        await emailField.FillAsync(email);

        await popup.GetByRole(AriaRole.Button, new()
        {
            Name = "Next"
        }).ClickAsync();

        Console.WriteLine("URL after email submission: " + popup.Url);
        Console.WriteLine("Page title: " + await popup.TitleAsync());
        Console.WriteLine("Google page content:");

        Console.WriteLine(
        await popup.Locator("body").InnerTextAsync()
        );

        await popup.ScreenshotAsync(new()
        {
            Path = "google-login-debug.png",
            FullPage = true
        });

        // Enter Google password
        var passwordField = popup.Locator("input[type='password']");

        await passwordField.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });

        await passwordField.FillAsync(password);

        await popup.GetByRole(AriaRole.Button, new()
        {
            Name = "Next"
        }).ClickAsync();

        // Wait for successful authentication and application redirect
        await Page.WaitForURLAsync(
            url => url.Contains("/home") ||
                   url.Contains("/stats") ||
                   url.Contains("/history") ||
                   url.Contains("/insights"),
            new()
            {
                Timeout = 60000,
                WaitUntil = WaitUntilState.DOMContentLoaded
            });
    }
}