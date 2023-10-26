## DAPR service invocation building block using httpClient
- checkout service
- order-processor service
## Run multi-app:
- cd service_invocation/csharp/http
- dapr run -f .
- dapr stop -f .
## Run a single app
    - cd ./order-processor
    - dapr run --app-port 5094 --app-id order-processor --app-protocol http --dapr-http-port 3501 -- dotnet run
    - cd ./checkout
    - dapr run --app-id checkout --app-protocol http --dapr-http-port 3500 -- dotnet run
## Stop and clean up application processes
    - dapr stop --app-id order-processor-http
    - dapr stop --app-id checkout-http