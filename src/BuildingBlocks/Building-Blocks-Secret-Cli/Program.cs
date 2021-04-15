using System;
using System.Net.Http;
using System.Threading.Tasks;
using Spectre.Console;

namespace Building_Blocks_Secret_Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HorizontalRule("Work with secrets");
     
            //run API for secrets file
            //dapr run --app-id my-secrets-app --dapr-http-port 3500 --components-path C:/Work/Dapr/gab-2021-dapr/src/BuildingBlocks/Building-Blocks-Secret-Cli/Components/
            var whichSecretStore = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which secret to [green]call[/]?")
                    .PageSize(3)
                    .AddChoice("Local")
                    .AddChoice("Azure KeyVault"));

            AnsiConsole.MarkupLine(
                $"Your selection is [blue]{whichSecretStore}[/], issuing call to that sidecar to get data..");
            var url = whichSecretStore == "Local"
                ? "http://localhost:3500/v1.0/secrets/localsecrets/my-secret"
                : "http://localhost:3500/v1.0/secrets/azurekeyvaultsecrets/SecretTest";
            
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