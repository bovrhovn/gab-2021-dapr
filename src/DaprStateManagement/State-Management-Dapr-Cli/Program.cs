using System;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using Spectre.Console;
using State_Management_Models;

namespace State_Management_Dapr_Cli
{
    class Program
    {
        /// <summary>
        /// run simple state management with Azure Tables
        /// </summary>
        /// <param name="args">any arguments from command line</param>
        /// <remarks>
        ///     in order to run the app, you'll need to run dapr before
        ///     dapr run --app-id state-management-app --dapr-grpc-port 50001 --components-path C:/Work/Dapr/gab-2021-dapr/src/DaprStateManagement/State-Management-Dapr-Cli/Components/
        /// </remarks>
        static async Task Main(string[] args)
        {
            const string storeName = "tablestorage";
            const string stateKeyName = "person-key";
            HorizontalRule("State management");
            using var client = new DaprClientBuilder().Build();

            var person = new Person {FullName = "Bojan Vrhovnik", Email = "bojan.vrhovnik@microsoft.com", Age = 30};

            AnsiConsole.MarkupLine($"Adding person [green]{person.FullName}[/] to the state");

            try
            {
                var cancellationToken = CancellationToken.None;
                await client.SaveStateAsync(storeName, stateKeyName, person, cancellationToken: cancellationToken);

                AnsiConsole.MarkupLine("Person added - let's retrieve the value");
                person = await client.GetStateAsync<Person>(storeName, stateKeyName, cancellationToken: cancellationToken);

                var table = new Table()
                    .AddColumn("Full name")
                    .AddColumn(new TableColumn("Email").Centered())
                    .AddColumn("Age");

                table.AddRow(person.FullName, person.Email, person.Age.ToString());
                AnsiConsole.Render(table);

                AnsiConsole.MarkupLine("Person will be deleted");
                await client.DeleteStateAsync(storeName, stateKeyName, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }
            AnsiConsole.MarkupLine("Person was deleted from state.");
        }

        private static void HorizontalRule(string title)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Render(new Rule($"[maroon]{title}[/]").RuleStyle("grey").LeftAligned());
            AnsiConsole.WriteLine();
        }
    }
}