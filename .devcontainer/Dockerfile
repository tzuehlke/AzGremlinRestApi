# Find the Dockerfile for mcr.microsoft.com/azure-functions/dotnet:3.0-dotnet3-core-tools at this URL
# https://github.com/Azure/azure-functions-docker/blob/master/host/3.0/buster/amd64/dotnet/dotnet-core-tools.Dockerfile
FROM mcr.microsoft.com/azure-functions/dotnet:3.0-dotnet3-core-tools

# Uncomment following lines If you want to enable Development Container Script
# For more details https://github.com/microsoft/vscode-dev-containers/tree/master/script-library

# Avoid warnings by switching to noninteractive
# ENV DEBIAN_FRONTEND=noninteractive

# # Comment out these lines if you want to use zsh.

# ARG INSTALL_ZSH=true
# ARG USERNAME=vscode
# ARG USER_UID=1000
# ARG USER_GID=$USER_UID

# RUN apt-get update && curl -ssL https://raw.githubusercontent.com/microsoft/vscode-dev-containers/master/script-library/common-debian.sh -o /tmp/common-script.sh \
#     && /bin/bash /tmp/common-script.sh "$INSTALL_ZSH" "$USERNAME" "$USER_UID" "$USER_GID" \
#     && rm /tmp/common-script.sh 

# install java
RUN sudo apt update && sudo apt install -y default-jdk

# install tinkerpop gremlin console in /workspaces/tinkerpop-cmd
WORKDIR /workspaces
RUN sudo chmod 777 -R /workspaces/ \
    && mkdir tinkerpop-cmd \
    && wget https://apache.mivzakim.net/tinkerpop/3.4.8/apache-tinkerpop-gremlin-console-3.4.8-bin.zip \
    && unzip apache-tinkerpop-gremlin-console-3.4.8-bin.zip \
    && mv apache-tinkerpop-gremlin-console-3.4.8/* tinkerpop-cmd/ \
    && rmdir apache-tinkerpop-gremlin-console-3.4.8

# config for function
ENV GRAPHDBHOSTNAME=<COSMOSDBACCOUNT>.gremlin.cosmos.azure.com
ENV GRAPHDBKEY=<KEY>
ENV GRAPHDBNAME=<COSMOSDB>
ENV GRAPHDBCOLL=<GRAPHCOLLECTION>
