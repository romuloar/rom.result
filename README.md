# Rom.Result

Welcome to **Rom.Result**
a flexible library for representing, composing, and returning rich operation results in .NET applications.
It provides a standardized way to encapsulate success, error, warning, and informational outcomes, including messages, parameters, timestamps, and exception details.

Supports .NET Standard 2.0 and .NET 9.

## 🔧 Why Rom.Result?

Traditional return types like bool, int, or even Exception don’t capture the full context of an operation.
**Rom.Result** enables you to return detailed, structured results from any method, making your APIs and business logic more expressive, 
testable, and maintainable.

## ✨ Features

- Strongly-typed result objects for Success, Error, Warning, and Info scenarios
- Rich metadata: messages, parameters, timestamps, and exception details
- Extension methods for easy and fluent result creation
- Async and sync support
- Works with any entity or value type

---

## Installation

>#### .NET CLI
```bash
dotnet add package Rom.Result
```
>#### Package Manager
```bash
Install-Package Rom.Result
```

## 📚 Table of Contents

| Group name      | Extensions | Group name      | Extensions |
|:---------------:|:----------|:---------------:|:-----------|
| **✅ Success** | 🔸 [GetResultDetailSuccess](#GetResultDetailSuccess)<br />🔸 [GetResultDetailSuccessAsync](#GetResultDetailSuccessAsync)<br />| **ℹ️ Info** | 🔸 [GetResultDetailInfo](#GetResultDetailInfo)<br />🔸 [GetResultDetailInfoAsync](#GetResultDetailInfoAsync)<br /> |
| **❌ Error** | 🔸 [GetResultDetailError](#GetResultDetailError)<br />🔸 [GetResultDetailErrorAsync](#GetResultDetailErrorAsync)<br /><br />**💥 Exception**<br />🔸 [GetResultDetailFromException](#GetResultDetailFromException)<br />🔸 [GetResultDetailFromExceptionAsync](#GetResultDetailFromExceptionAsync)<br />| **⚠️ Warning** | 🔸 [GetResultDetailWarning](#GetResultDetailWarning)<br />🔸 [GetResultDetailWarningAsync](#GetResultDetailWarningAsync)<br /> |
| **⚙️ ResultDetailExtensions** | 🔸 [GetError](#GetError)<br />🔸 [GetErrorAsync](#GetErrorAsync)<br /><br />| |


### ✅ Success

## GetResultDetailSuccess
Returns a detailed success result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp

// Example 1: Basic usage with an entity
var entity = new { Id = 1, Name = "Test" };
var result = entity.GetResultDetailSuccess();
/* Output:
{
  "resultType": "Success",
  "parameters": null,
  "isSuccess": true,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1, "Name": "Test" }
} 
*/

// Example 2: With message and date
var result = entity.GetResultDetailSuccess("Operation completed", "2024-01-01T12:00:00Z");
/* Output:
{
  "resultType": "Success",
  "parameters": null,
  "isSuccess": true,
  "message": "Operation completed",
  "timestamp": "2024-01-01T12:00:00.000Z",
  "resultData": { "Id": 1, "Name": "Test" }
}
*/

// Example 3: With parameters
int id = 1;
string name = "Test";
var result = entity.GetResultDetailSuccess("With params", null, () => id, () => name);
/* Output:
{
  "resultType": "Success",
  "parameters": { "id": 1, "name": "Test" },
  "isSuccess": true,
  "message": "With params",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1, "Name": "Test" }
}
*/
```

## GetResultDetailSuccessAsync
Asynchronously returns a detailed success result for the specified entity, optionally with message, date, and parameters..

### Examples:
```csharp
// Example 1: Async usage
var entity = new { Id = 1 };
var result = await entity.GetResultDetailSuccessAsync();
/* Output:
{
  "resultType": "Success",
  "parameters": null,
  "isSuccess": true,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/

// Example 2: Async with message and parameters
int code = 200;
var result = await entity.GetResultDetailSuccessAsync("Async success", null, () => code);
/* Output:
{
  "resultType": "Success",
  "parameters": { "code": 200 },
  "isSuccess": true,
  "message": "Async success",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/

```

### ❌ Error

## GetResultDetailError
Returns a detailed error result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp
// Example 1: Basic error result
var entity = new { Id = 1 };
var result = entity.GetResultDetailError();
/* Output:
{
  "resultType": "Error",
  "parameters": null,
  "isSuccess": false,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
// Example 2: With error message
var result = entity.GetResultDetailError("An error occurred");
/* Output:
{
  "resultType": "Error",
  "parameters": null,
  "isSuccess": false,
  "message": "An error occurred",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
// Example 3: With parameters
string reason = "Invalid";
var result = entity.GetResultDetailError("Error with params", null, () => reason);
/* Output:
{
  "resultType": "Error",
  "parameters": { "reason": "Invalid" },
  "isSuccess": false,
  "message": "Error with params",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
```

## GetResultDetailErrorAsync
Asynchronously returns a detailed error result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp
// Example 1: Async error result
var entity = new { Id = 1 };
var result = await entity.GetResultDetailErrorAsync();
/* Output:
{
  "resultType": "Error",
  "parameters": null,
  "isSuccess": false,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/

// Example 2: Async with message and parameters
int code = 500;
var result = await entity.GetResultDetailErrorAsync("Async error", null, () => code);
/* Output:
{
  "resultType": "Error",
  "parameters": { "code": 500 },
  "isSuccess": false,
  "message": "Async error",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
```

### 💥 Exception

## GetResultDetailFromException
Returns a detailed error result for the specified exception, optionally with parameters.

### Examples:
```csharp
// Example 1: Basic exception usage
try
{
    throw new InvalidOperationException("Something failed");
}
catch (Exception ex)
{
    var result = ex.GetResultDetailFromException();
    // Output:
    // {
    //   "resultType": "Error",
    //   "parameters": null,
    //   "isSuccess": false,
    //   "message": "Something failed",
    //   "timestamp": "2024-05-25T12:00:00.000Z",
    //   "resultData": null
    // }
}

// Example 2: Exception with parameters
int userId = 42;
try
{
    throw new Exception("User not found");
}
catch (Exception ex)
{
    var result = ex.GetResultDetailFromException(() => userId);
    // Output:
    // {
    //   "resultType": "Error",
    //   "parameters": { "userId": 42 },
    //   "isSuccess": false,
    //   "message": "User not found",
    //   "timestamp": "2024-05-25T12:00:00.000Z",
    //   "resultData": null
    // }
}
```

## GetResultDetailFromExceptionAsync
Asynchronously returns a detailed error result for the specified exception, optionally with parameters.

### Examples:
```csharp
// Example 1: Async exception usage
try
{
    throw new InvalidOperationException("Async fail");
}
catch (Exception ex)
{
    var result = await ex.GetResultDetailFromExceptionAsync();
    // Output:
    // {
    //   "resultType": "Error",
    //   "parameters": null,
    //   "isSuccess": false,
    //   "message": "Async fail",
    //   "timestamp": "2024-05-25T12:00:00.000Z",
    //   "resultData": null
    // }
}

// Example 2: Async exception with parameters
string operation = "Delete";
try
{
    throw new Exception("Operation failed");
}
catch (Exception ex)
{
    var result = await ex.GetResultDetailFromExceptionAsync(() => operation);
    // Output:
    // {
    //   "resultType": "Error",
    //   "parameters": { "operation": "Delete" },
    //   "isSuccess": false,
    //   "message": "Operation failed",
    //   "timestamp": "2024-05-25T12:00:00.000Z",
    //   "resultData": null
    // }
}
```

### ⚙ ResultDetailExtensions

## GetError
Creates a ResultDetail<T> with error result type, message, and optional date and parameters.

### Examples:
```csharp
// Example 1: Basic error result
var result = ResultDetailExtension.GetError<string>("An error occurred");
// Output:
// {
//   "resultType": "Error",
//   "parameters": null,
//   "isSuccess": false,
//   "message": "An error occurred",
//   "timestamp": "2024-05-26T12:00:00.000Z",
//   "resultData": null
// }

// Example 2: With date
var result = ResultDetailExtension.GetError<int>("Error with date", "2024-01-01T12:00:00Z");
// Output:
// {
//   "resultType": "Error",
//   "parameters": null,
//   "isSuccess": false,
//   "message": "Error with date",
//   "timestamp": "2024-01-01T12:00:00.000Z",
//   "resultData": 0
// }

// Example 3: With parameters
int code = 404;
var result = ResultDetailExtension.GetError<object>("Error with params", () => code);
// Output:
// {
//   "resultType": "Error",
//   "parameters": { "code": 404 },
//   "isSuccess": false,
//   "message": "Error with params",
//   "timestamp": "2024-05-26T12:00:00.000Z",
//   "resultData": null
// }
*/
```

## GetErrorAsync
Asynchronously creates a ResultDetail<T> with error result type, message, and optional date and parameters.

### Examples:
```csharp
// Example 1: Basic error result
// Example 1: Async error result
var result = await ResultDetailExtension.GetErrorAsync<string>("Async error");
// Output:
// {
//   "resultType": "Error",
//   "parameters": null,
//   "isSuccess": false,
//   "message": "Async error",
//   "timestamp": "2024-05-26T12:00:00.000Z",
//   "resultData": null
// }

// Example 2: Async with date and parameters
int value = 123;
var result = await ResultDetailExtension.GetErrorAsync<int>("Async error with params", "2024-01-01T12:00:00Z", () => value);
// Output:
// {
//   "resultType": "Error",
//   "parameters": { "value": 123 },
//   "isSuccess": false,
//   "message": "Async error with params",
//   "timestamp": "2024-01-01T12:00:00.000Z",
//   "resultData": 0
// }
*/
```

### ℹ️ Info

## GetResultDetailInfo
Returns a detailed informational result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp
// Example 1: Basic usage with an entity
var entity = new { Id = 1, Name = "Test" };
var result = entity.GetResultDetailInfo();
/* Output:
{
  "resultType": "Info",
  "parameters": null,
  "isSuccess": true,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1, "Name": "Test" }
}
*/

// Example 2: With message and date
var result = entity.GetResultDetailInfo("Entity loaded", "2024-01-01T12:00:00Z");
/* Output:
{
  "resultType": "Info",
  "parameters": null,
  "isSuccess": true,
  "message": "Entity loaded",
  "timestamp": "2024-01-01T12:00:00.000Z",
  "resultData": { "Id": 1, "Name": "Test" }
}
*/

// Example 3: With parameters
int id = 1;
string name = "Test";
var result = entity.GetResultDetailInfo("With params", null, () => id, () => name);
/* Output:
{
  "resultType": "Info",
  "parameters": { "id": 1, "name": "Test" },
  "isSuccess": true,
  "message": "With params",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1, "Name": "Test" }
}
*/
```

## GetResultDetailInfoAsync
Asynchronously returns a detailed informational result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp
// Example 1: Async usage
var entity = new { Id = 1 };
var result = await entity.GetResultDetailInfoAsync();
/* Output:
{
  "resultType": "Info",
  "parameters": null,
  "isSuccess": true,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/

// Example 2: Async with message and parameters
int code = 200;
var result = await entity.GetResultDetailInfoAsync("Async info", null, () => code);
/* Output:
{
  "resultType": "Info",
  "parameters": { "code": 200 },
  "isSuccess": true,
  "message": "Async info",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
```

### ⚠️ Warning

## GetResultDetailWarning
Returns a detailed warning result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp
// Example 1: Basic warning result
var entity = new { Id = 1 };
var result = entity.GetResultDetailWarning();
/* Output:
{
  "resultType": "Warning",
  "parameters": null,
  "isSuccess": true,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
// Example 2: With warning message
var result = entity.GetResultDetailWarning("Check this value");
/* Output:
{
  "resultType": "Warning",
  "parameters": null,
  "isSuccess": true,
  "message": "Check this value",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
// Example 3: With parameters
string field = "Age";
var result = entity.GetResultDetailWarning("Field warning", null, () => field);
/* Output:
{
  "resultType": "Warning",
  "parameters": { "field": "Age" },
  "isSuccess": true,
  "message": "Field warning",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
```

## GetResultDetailWarningAsync
Asynchronously returns a detailed warning result for the specified entity, optionally with message, date, and parameters.

### Examples:
```csharp
// Example 1: Async warning result
var entity = new { Id = 1 };
var result = await entity.GetResultDetailWarningAsync();
/* Output:
{
  "resultType": "Warning",
  "parameters": null,
  "isSuccess": true,
  "message": null,
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
// Example 2: Async with message and parameters
string field = "Status";
var result = await entity.GetResultDetailWarningAsync("Async warning", null, () => field);
/* Output:
{
  "resultType": "Warning",
  "parameters": { "field": "Status" },
  "isSuccess": true,
  "message": "Async warning",
  "timestamp": "2024-05-25T12:00:00.000Z",
  "resultData": { "Id": 1 }
}
*/
```

## Contribution

Contributions are welcome! Please feel free to submit a Pull Request.

## License

MIT

## Author

Romulo Ribeiro
