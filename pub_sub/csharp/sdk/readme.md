## DAPR pub_sub building block using SDK
- publisher: checkout service
- subscriber: order-processor service
## Run multi-app:
- cd pub_sub/csharp/sdk
- dapr run -f .
- dapr stop -f .
## Run a single app
- Subscriber
    - cd ./order-processor
    - dapr run --app-id order-processor-sdk --resources-path ../../../components/ --app-port 5138 -- dotnet run
- Publisher
    - cd ./checkout
    - dapr run --app-id checkout-sdk --resources-path ../../../components/ -- dotnet run
## Stop and clean up application processes
    - dapr stop --app-id order-processor-sdk
    - dapr stop --app-id checkout-sdk

## Ref: https://docs.dapr.io/getting-started/quickstarts/pubsub-quickstart/