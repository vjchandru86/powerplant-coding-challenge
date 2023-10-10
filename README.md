# Introduction
This guide provides the details about the solution and instructions to run this project in your machine.

# Requirements
- This project is created in .Net 6. To run this project locally, you need Visual studio 2022 or Docker desktop installed in your machine.

# Swagger
Swagger UI is added in this project. http://localhost:8888/swagger will take you to the swagger UI. But this works only when ASPNETCORE_ENVIRONMENT is set to "Development".


# If you have VS 2022 in your machine

## Steps
- Dowload this project from the Github.
- Unzip the files and open the solution file, PowerPlantCC.sln using VS 2022.
- Build the solution to restore the nuget packages.
- Run the project using kestrel/IIS express and you should be able to view the swagger UI.(http://localhost:8888/swagger/index.html)
- The endpoint for this code challenge is http://localhost:8888/productionplan, which is a POST method.

# If you have Docker Desktop
This project is also provided with Dockerfile. If your machine has Docker Desktop installed, this is another option to run the solution in a container. The default target OS is set to Linux.

## Requirements
- Need to have Docker Desktop installed with Linux container

## Steps
- Dowload this project from the Github.
- Unzip the files and go to the folder of the solution file, PowerPlantCC.sln.
- Open Command Propmt in this folder.
- Run the command `docker build -t nameoftheimage:tag -f src\PowerPlantCC\Dockerfile .`
- After the image is built successfully, run the command `docker run -d -p 8888:80 -e ASPNETCORE_URLS="http://+:80" -e ASPNETCORE_ENVIRONMENT="Development"  nameoftheimage:tag`.
- Once the command is executed successfully, Swagger UI can be accessed via http://localhost:8888/swagger/index.html and the endpoint should be accessible via http://localhost:8888/productionplan.