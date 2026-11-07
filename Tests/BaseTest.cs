using ExpenseTracker.Automation.Pages;
using Microsoft.Playwright.NUnit;

namespace ExpenseTracker.Automation.Tests;

public abstract class BaseTest : PageTest
{
    protected LoginPage LoginPage => new(Page);
}
