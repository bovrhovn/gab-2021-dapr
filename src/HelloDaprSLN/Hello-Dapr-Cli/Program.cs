using System;
using System.Net.Http;
using System.Threading.Tasks;
using Spectre.Console;

namespace Hello_Dapr_Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HorizontalRule("Hello Dapr");
            var whichSideCarToCall = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which sidecar to [green]call[/]?")
                    .PageSize(3)
                    .AddChoice("Default sidecar")
                    .AddChoice("With custom components"));

            AnsiConsole.MarkupLine(
                $"Your selection is [blue]{whichSideCarToCall}[/], issuing call to that sidecar to get data..");
            var url = whichSideCarToCall == "Default sidecar"
                ? "http://localhost:3600/v1.0/state/statestore/key1"
                : "http://localhost:3700/v1.0/state/statestore/key2";
            using var httpClient = new HttpClient();
            try
            {
                AnsiConsole.MarkupLine($"Issuing http call to [green]{url}[/]");
                var responseMessage = await httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                var keyValue = await responseMessage.Content.ReadAsStringAsync();
                AnsiConsole.WriteLine(keyValue);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }
        }

        private static void HorizontalRule(string title)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Render(new Rule($"[maroon]{title}[/]").RuleStyle("grey").LeftAligned());
            AnsiConsole.WriteLine();
        }
    }
}