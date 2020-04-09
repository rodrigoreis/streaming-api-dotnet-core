# streaming-api-dotnet-core
## proposal
demonstrates a realtime stream API from scratch using dotnet core

## dependencies
- netcoreapp3.1

## usage

open the URL below:

```
https://localhost:5001/api/streaming
```

Make a POST or PUT request and see the URL opened above

```shell
curl --location --request POST 'https://localhost:5001/api/streaming' \
--header 'Content-Type: application/json' \
--data-raw '{
	"id":"1",
	"name":"this is a POST request"
}'
```

```shell
curl --location --request PUT 'https://localhost:5001/api/streaming' \
--header 'Content-Type: application/json' \
--data-raw '{
	"id":"1",
	"name":"this is a PUT request"
}'
```
