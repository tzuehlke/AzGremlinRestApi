# AzGremlinRestApi
Provide simple REST API as Azure Function for Gremlin Cosmos DB in a [Remote-Container](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) (VSCode extension)
![gremlin-overview](https://user-images.githubusercontent.com/32843441/103159777-d86fb780-47cd-11eb-9952-ea0242e0ab9d.png)

# Configuration
## Azure Function
1. modify Dockerfile lines:
```
# config for function
ENV GRAPHDBHOSTNAME=<COSMOSDBACCOUNT>.gremlin.cosmos.azure.com
ENV GRAPHDBKEY=<KEY>
ENV GRAPHDBNAME=<COSMOSDB>
ENV GRAPHDBCOLL=<GRAPHCOLLECTION>
```
2. Rebuild container

## Tinkerpop Console
modify `remote-secure.yaml` lines:
```
hosts: [<COSMOSDBACCOUNT>.gremlin.cosmos.azure.com]
port: 443
username: /dbs/<COSMOSDB>/colls/<GRAPHCOLLECTION>
password: <KEY>
```

# Using
## Calling Azure Function
calling as POST request with body:
```JSON
{
    "query": "g.V().limit(3)"
}
```

## Using Tinkerpop Console
1. Navigate to: `/workspaces/tinkerpop-cmd`
2. start console: `./gremlin.sh`
3. connect to Azure DB: `:remote connect tinkerpop.server /workspaces/AzGremlinRestApi/remote-secure.yaml`
4. send query: `:> g.V().limit(3)`
5. exit console: `:exit`