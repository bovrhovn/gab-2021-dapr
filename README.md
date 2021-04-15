# Global Azure Bootcamp 2021 - building blocks

This repository contains demos for my session on [Dapr](https://dapr.io/) (distributed application runtime) for [Global Azure Bootcamp 2021 event](https://globalazure.net/). Talk is in Slovenian language as part of Slovenian Azure User Group initiative. 

Demos are showing:
1. usage of secrets stores (how to use them and call them)
2. usage of pub/sub system (how to use default one and integrate with Azure Services)
3. usage of state options (on [this branch](https://github.com/bovrhovn/gab-2021-dapr/tree/state-management))

In order to run the demo, you will need to create dapr applicaton and then run the applications (`dotnet run`). In order to run the web application, you will need to supply SignalR Connection string -  **Azure:SignalR:ConnectionString**. How to do that, check tutorial [here](https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-quickstart-dotnet-core). 

```
dapr run --app-id dapr-sb --components path ./Components --dapr-grpc-port 50001
```

All demos are simple to prove the concepts, main features and capabilities. 

# Credits

During the demos following solutions were used:
1. [Spectre.Console](https://github.com/spectreconsole/spectre.console)


# Additional information

Official Dapr documentation has a [FAQ](https://docs.dapr.io/concepts/faq/) section and link to [Discord forum](https://discord.com/invite/ptHhX6jc34), where you can ask additional questions about Dapr in general or to meet fellow dapr devs.

Check out [quickstarts](https://github.com/dapr/quickstarts) as well.
