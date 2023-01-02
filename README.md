Built and tested using macOS m1 13.0.1

# How to run

**Important: You'll need docker installed or dotnet 6 installed.**

After cloning, on the root folder run the following commands:

`docker-compose build`, then run: `docker-compose up`.

The application will be running in the port http 5010.

If you are running outside docker, you'll need an sql database installed and also to set up the connection string in the appsettings.json file.
