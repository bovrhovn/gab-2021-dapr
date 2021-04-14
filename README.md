# [Dapr + Azure](https://globalazure.net/sessions/250682) - State management building blocks examples

This repository contains demos about how to start with a building block in Dapr. My weapon of choice will be [.NET](https://dot.net) Core framework.

## Simple web call to the Dapr side car

As you already saw, Dapr uses [Sidecar pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/sidecar) to help you with most commong programming
tasks. To be platform agnostic, you can connect to that sidecar via HTTP or gRPC call.

With that, you can easily call an API via [Postman](https://www.postman.com/) or whatever tool can help you issue REST calls ([HttpRepl](https://github.com/dotnet/HttpRepl), [Insomnia](https://insomnia.rest/), ...), or just use CLI (Bash, Powershell).

We will be using Powershell to do our call to the repo.

Let us use defaults and create sidecar containers for communication:

`dapr run --app-id hello-dapr --dapr-http-port 3600`

`Invoke-RestMethod -Method Post -ContentType 'application/json' -Body '[{ "key": "key1", "value": "I am sidecar state saver"}]' -Uri 'http://localhost:3600/v1.0/state/statestore'`

And let use custom folder with custom definitions:

`dapr run --app-id hello-dapr-custom --dapr-http-port 3700 --components-path ./dev-components`

`Invoke-RestMethod -Method Post -ContentType 'application/json' -Body '[{ "key": "key2", "value": "I am sidecar dev component state saver"}]' -Uri 'http://localhost:3700/v1.0/state/dev-statestore'`

*Reminder: you need to specify folder path to the components*

Run the console application **Hello-Dapr-Cli** to get the state from different option (without knowing the implementation).

Also, as an example, how this can be used in Kubernetes, you can check awesome example - [distributed calculator](https://github.com/dapr/quickstarts/tree/v1.0.0/distributed-calculator).

# CREDITS

All credits are located on [main branch](https://github.com/bovrhovn/gab-2021-dapr). 

Check out [quickstarts](https://github.com/dapr/quickstarts) as well.

[.NET SDK for Dapr](https://github.com/dapr/dotnet-sdk) is available [here](https://github.com/dapr/dotnet-sdk).

