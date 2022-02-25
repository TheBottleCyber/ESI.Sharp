[![.NET Sonar Scanner](https://github.com/TheBottleCyber/ESI.Sharp/actions/workflows/sonar.yml/badge.svg)](https://github.com/TheBottleCyber/ESI.Sharp/actions/workflows/sonar.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TheBottleCyber_ESI.Sharp&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TheBottleCyber_ESI.Sharp)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=TheBottleCyber_ESI.Sharp&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=TheBottleCyber_ESI.Sharp)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=TheBottleCyber_ESI.Sharp&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=TheBottleCyber_ESI.Sharp)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=TheBottleCyber_ESI.Sharp&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=TheBottleCyber_ESI.Sharp)
<a href="https://www.nuget.org/packages/ESI.Sharp/">
    <img src="https://img.shields.io/nuget/vpre/ESI.Sharp.svg?maxAge=2592000?style=badge" alt="NuGet">
</a>
# What is ESI.Sharp
ESI.Sharp is an unofficial .NET API Wrapper for the EVE Online ESI (https://esi.evetech.net/ui) with **powerfully** commented endpoints, models and enumerations
## Resources

* [ESI-Docs](https://docs.esi.evetech.net/) ([source](https://github.com/esi/esi-docs)) - This is the best documentation concerning ESI and the SSO process.
* [ESI Application Keys](https://developers.eveonline.com/)
* [ESI Swagger Definition](https://esi.evetech.net/latest/swagger.json)

## Install
You can install **ESI.Sharp** as [a nuget package](https://www.nuget.org/packages/ESI.Sharp):
<br>
`dotnet add package ESI.Sharp`

**ESI.Sharp** is a .NET Standard 2.0 Library with support for .NET Core 5.0+

## Usage
You can instantiate the client in this manner:

```c#
var esiConfig = new EsiConfig(clientId: "*******", secretKey: "*******", callbackUrl: "", userAgent: "");
var esiClient = new EsiClient(esiConfig);
```
*For your protection (and mine), you are required to supply a userAgent value. 
This can be your character name and/or project name. 
CCP will be more likely to contact you than just cut off access to ESI if you provide something that can identify you within the New Eden galaxy.
Without this property populated, the wrapper will not work.*

### Status public endpoint example
Accessing a public endpoint is extremely simple:
```c#
var status = await esiClient.Status.Retrieve();
var statusJsoned = JsonConvert.SerializeObject(status);

Console.WriteLine(statusJsoned);
```

And it will print **EsiResponse** object to console output:
```elixir
{
  "RequestId": "e26f94ed-03ae-4d82-8bc8-ba68f357b2a5",
  "StatusCode": 200,
  "Expires": "2022-02-21T01:45:04Z",
  "LastModified": "2022-02-21T01:44:34Z",
  "ETag": "60f0724159e0f94524abc3abcd8ffda1ba5bb2079e64de28b32aa659",
  "ErrorLimitRemain": 100,
  "ErrorLimitReset": 18,
  "Pages": null,
  "Message": null,
  "Data": {
    "players": 18024,
    "server_version": "2003445",
    "start_time": "2022-02-20T11:01:33Z",
    "vip": false
  },
  "Exception": null
}
```

### SSO Authorization
#### SSO Login URL generator
**ESI.Sharp** has method to generate the URL required to authenticate a character or authorize roles (by providing a params of scopes) for the ESI API.

You should also provide a value for **state** that you verify when it is returned (it will be included in the callback).

Without scopes (only **public** endpoints will available):
```c#
var authUrl = esiClient.Authorization.CreateAuthorizationUrl("custom_state");
```
With all scopes (all **authorized** endpoints will available):
```c#
var allScopes = EnumHelper.GetEnumValues<Scope>();
var authUrl = esiClient.Authorization.CreateAuthorizationUrl("custom_state", allScopes.ToArray());
```
With custom scopes params (only **allowed by scope** endpoints will available):
```c#
var authUrl = esiClient.Authorization.CreateAuthorizationUrl("custom_state",
 Scope.UIOpenWindow, Scope.AssetsReadAssets, Scope.CharactersReadTitles, ...);
```

#### Initial SSO token request
```c#
var token = await esiClient.Authorization.GetToken(TokenGrantType.AuthorizationCode, response_code);
var validatedToken = await client.Authorization.ValidateToken(token);
```
#### Refresh SSO token
```c#
var token = await esiClient.Authorization.GetToken(TokenGrantType.RefreshToken, refresh_token);
```
#### Performing an authenticated request
Set the validated token data on the client before performing the request
```c#
esiClient.SetRequestToken(validatedToken);
```