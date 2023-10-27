# Dapr state management

1. Open a new terminal window and run  `order-processor` using the multi app run template defined in [dapr.yaml](./dapr.yaml):

2. Run the Dotnet service app with Dapr:

dapr run -f .

3. Stop and clean up application processes

dapr stop -f .

## Run a single app at a time with Dapr (Optional)

1. Run the Dotnet service app with Dapr:

cd ./order-processor
dapr run --app-id order-processor --resources-path ../../../resources/ -- dotnet run

2. Stop and clean up application processes

dapr stop --app-id order-processor