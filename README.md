# [Dapr + Azure](https://globalazure.net/sessions/250682) - State management building blocks examples

This repository contains demos about how to start with a building block in Dapr. My weapon of choice will be [.NET](https://dot.net) Core framework.

## Call to get state data behind the scenes

As you already saw, Dapr uses [Sidecar pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/sidecar) to help you with most commong programming
tasks. To be platform agnostic, you can connect to that sidecar via HTTP or gRPC call.

How to do the call with http api and invoke http or grpc call, check [Hello Dapr branch](https://github.com/bovrhovn/gab-2021-dapr/tree/hello-dapr).

# RUN THE CODE

1. In order to run the CLI demo, you will need to configure port via environment variable to reflect the port on which sidecar is listening. You can set **DAPR_GRPC_PORT** environment variable for app to connect to that grpc endpoint. In order to use table storage component, you will need to create azure storage account and populate values with data inside [tablestorage.yaml](https://github.com/bovrhovn/gab-2021-dapr/blob/state-management/src/DaprStateManagement/State-Management-Dapr-Cli/Components/tablestorage.yaml) file 
2. In order to run the web page demo, you will need to provide an url to the web api site. You can do that directly in **appSettings.json** or you can enter data through environment variables **WebApiLink** (baseline link with ending /).
3. You can run it as well as docker containers. Check folder Docker and build the image `docker build yourimagename -f Dockername .` After that you can easily run the image with [docker run](https://docs.docker.com/engine/reference/commandline/run/) command. Don't forget to pass in [environment names](https://docs.docker.com/engine/reference/commandline/run/#set-environment-variables--e---env---env-file) (WebApiLink)

# CREDITS

All credits are located on [main branch](https://github.com/bovrhovn/gab-2021-dapr). 

Check out [quickstarts](https://github.com/dapr/quickstarts) as well.

[.NET SDK for Dapr](https://github.com/dapr/dotnet-sdk) is available [here](https://github.com/dapr/dotnet-sdk).

