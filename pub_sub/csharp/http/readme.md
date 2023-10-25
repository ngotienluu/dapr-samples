## DAPR pub_sub building block using httpClient
- publisher: checkout service
- subscriber: order-processor service
## Run multi-app:
- cd pub_sub/csharp/http
- dapr run -f .
- dapr stop -f .
## Run a single app
- Subscriber
    - cd ./order-processor
    - dapr run --app-id order-processor-http --resources-path ../../../components/ --app-port 5109 -- dotnet run
- Publisher
    - cd ./checkout
    - dapr run --app-id checkout-http --resources-path ../../../components/ -- dotnet run
## Stop and clean up application processes
    - dapr stop --app-id order-processor-http
    - dapr stop --app-id checkout-http

## Ref: https://docs.dapr.io/getting-started/quickstarts/pubsub-quickstart/