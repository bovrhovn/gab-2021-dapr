using System;
using System.Threading;
using System.Threading.Tasks;
using Building_Blocks_Models;
using Dapr.Client;
using Spectre.Console;

namespace Building_Blocks_Cli
{
    /// <summary>
    /// sending pub/sub via Azure Service Bus
    /// </summary>
    /// <example>
    ///     To run manually, use below commands:
    ///     dapr run --app-id dapr-sb --components path ./Components --dapr-grpc-port 50001
    ///     Invoke-RestMethod -Method Post -ContentType 'application/json' -Body '{"status": "completed"}' -Uri 'http://localhost:3500/v1.0/publish/pubsub/deathStarStatus'
    /// </example>
    class Program
    {
        static async Task Main(string[] args)
        {
            HorizontalRule("Building blocks - pub sub");
            //var pubsubName = "servicebus-pubsub";
            var pubsubName = "pubsub";
            //publish events via service bus
            using var client = new DaprClientBuilder().Build();

            var cancellationToken = CancellationToken.None;

            AnsiConsole.MarkupLine("Sending random events to the topic");

            try
            {
                for (int counter = 1; counter <= 20; counter++)
                {
                    var eventData = new AmmountMessage {Id = counter, Ammount = 100 * counter};
                    await client.PublishEventAsync(pubsubName, "messages", eventData, cancellationToken);
                    AnsiConsole.WriteLine($"We published an event with id {counter}");
                }
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }

            AnsiConsole.MarkupLine("Finished sending [green]all[/] events");
        }

        private static void HorizontalRule(string title)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Render(new Rule($"[maroon]{title}[/]").RuleStyle("grey").LeftAligned());
            AnsiConsole.WriteLine();
        }
    }
}