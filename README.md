<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li>
            <a href="#installation-and-running">Installation and Running</a>
            <ul>
                <li><a href="#running-with-dotnet-cli">Running With Dotnet Cli</li>
            </ul>
            <ul>
                <li><a href="#running-with-docker-compose">Running With docker-compose</li>
            </ul>
            <ul>
                <li><a href="#running-with-kubernetes">Running With Kubernetes</li>
            </ul>
        </li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

This project was created for using postgis with asp.net core. It is a simple project that provides a service to CRUD operations for streets. It uses postgis for spatial data operations.

### Built With

* ASP.NET 9.0
* Kind
* Kubernetes
* Docker
* PostgreSql with PostGIS

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

* Visual Studio or Visual Studio Code
* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* Docker
* Kind

### Installation and Running

#### Running With Dotnet Cli

Follow these steps;

1. Build the project
   ```sh
   dotnet build
   ```
2. Run the project
    ```sh
    dotnet run
    ```

#### Running With docker-compose

Follow these steps;

1. Build the project
   ```sh
   docker-compose up
   ```

#### Running With Kubernetes

Follow these steps;

1. Create a kind cluster
   ```sh
   kind create cluster --config .\Kubernetes\Kind\kubernetes-cluster-config.yaml
   ```
2. Build docker image

   ```sh
   docker build -t streetservice:0.1 .
   ```
3. Load image to kind cluster
   ```sh
   kind load docker-image streetservice:0.1
   ```
4. Deploy the project
   ```sh
   kubectl apply -f .\Kubernetes\deployment.yaml
   ```
5. Port forward to access the service
   ```sh
   kubectl port-forward deployment/StreetService-deployment -n test 8080:8080
   ```


<!-- CONTACT -->
## Contact

Tugay Ersoy - [@Admiralkheir](https://x.com/Admiralkheir) - tugay.ersoy@gmail.com

<p align="right">(<a href="#about-the-project">back to top</a>)</p>
