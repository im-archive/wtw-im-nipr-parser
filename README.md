# Nipr Parser #

The [National Insurance Producer Registry](https://www.nipr.com) provides a paid subscription service to their [Producer Database (PDB)](https://www.nipr.com/index_nipr_producer_database.htm). This service accepts `x-www-form-urlencoded` encoded requests and returns an XML document with the requested data.

This .NET packages serves as both a serializer/deserializer for the two types of respones (`HITLIST` and `PDB`) as well as helper methods to read/write the files to disk and extension methods to easily retrieve and manipulate the data.

## Serializing and Deserializing ##


## `HITLIST` Extension Methods ##

## `PDB` Extension Methods ##

## Error Responses ##

If a request for either a `HITLIST` or `PDB` returns an error, the `TransactionType` property is set to `ERROR` and the `Error` property will contain a description of the error. Both `HitList` and `Pdb` have extensions methods to detect if the response is an error and to retrieve the error message and factory constructors to generate error responses.

## Generating an Error Response ###

>The string `User, NotValid, does not exist or is not authorized.` is currently what is returned by the service when the user credentials supplied in the request are invalid.

### Generating Error Responses for `HitList` ###

You can easily generate an error response for `HitList` using the static factory constructor `HitList.GenerateError(string)`. The following code:

```csharp
var hitlist = HitList.GenerateError("User, NotValid, does not exist or is not authorized.");
```

is equivalent to:

```csharp
var hitlist = new HitList
{
    TransactionType = new TransactionType { Type = TransactionTypes.Error },
    Error = new Error { Description = "User, NotValid, does not exist or is not authorized." }
};
```

and when serialized will return the following XML:

```xml
<?xml version='1.0' encoding='UTF-8'?>
<HITLIST>
    <TRANSACTION_TYPE>
        <TYPE>ERROR</TYPE>
    </TRANSACTION_TYPE>
    <ERROR>
        <DESCRIPTION>User, NotValid, does not exist or is not authorized.</DESCRIPTION>
    </ERROR>
</HITLIST>
```

### Generating Error Responses for `Pdb` ###

You can easily generate an error response for `Pdb` using the static factory constructor `Pdb.GenerateError(string)`. The following code:

```csharp
var pdb = Pdb.GenerateError("User, NotValid, does not exist or is not authorized.");
```

is equivalent to:

```csharp
var pdb = new Pdb
{
    TransactionType = new TransactionType { Type = TransactionTypes.Error },
    Error = new Error { Description = "User, NotValid, does not exist or is not authorized." }
};
```

and when serialized will return the following XML:

```xml
<?xml version='1.0' encoding='UTF-8'?>
<PDB>
    <TRANSACTION_TYPE>
        <TYPE>ERROR</TYPE>
    </TRANSACTION_TYPE>
    <ERROR>
        <DESCRIPTION>User, NotValid, does not exist or is not authorized.</DESCRIPTION>
    </ERROR>
</PDB>
```

### Checking Responses For Errors ###

To easily check for an error response, use the `IsErrorResponse()` extension method. If the response is an error response, an error message is stored in `Error.Description`, but can be accessesed more conveniently with the `GetErrorMessage()` extension method.

#### Checking `HITLIST` Responses For Errors ####

```csharp
var hitlist = HitList.GenerateError("User, NotValid, does not exist or is not authorized.");

if (hitlist.IsErrorResponse()) // will return true
{
    // will output "User, NotValid, does not exist or is not authorized."
    Console.WriteLine(hitlist.GetErrorMessage());
}
```

#### Checking `PDB` Responses For Errors ####

```csharp
var pdb = Pdb.GenerateError("User, NotValid, does not exist or is not authorized.");

if (pdb.IsErrorResponse()) // will return true
{
    // will output "User, NotValid, does not exist or is not authorized."
    Console.WriteLine(pdb.GetErrorMessage());
}
```