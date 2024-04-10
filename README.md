# BasicAuthHandler
This project aims to study verification events in Basic authorization for APIs in C# ASP.NET Core.

***Please note that this project is not intended for modification or implementation in a production environment; it serves solely for extracting concepts to develop your own project.***

## Description
This project utilizes C# language and .NET version 8 and is designed to restrict access to an API using Basic authentication (username and password). Any request with a username and password different from the default ones accepted will be rejected.

### Accepted Users and Passwords
You can find the list of users and passwords accepted by the API in the JSON file located at Content/AcceptUserKeys.json.

## Installation
To test and analyze this project, simply clone it to your machine:

```
git clone https://github.com/ErikFernandes/BasicAuthHandler.git
```

### Configuration
If you wish to host the API for testing on your machine, after cloning the repository, you need to edit the Properties/launchSettings.json file and change the IP to your current IP and the desired port.

## License
This project is licensed under the [MIT License](https://spdx.org/licenses/MIT.html).
