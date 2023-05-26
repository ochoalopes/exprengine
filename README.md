# ExprEngine

ExprEngine is a lightweight expression evaluation library that allows you to validate, evaluate, and compute mathematical expressions with support for variables.

## Getting Started

### Installation

The ExprEngine library can be installed via NuGet. Use the package manager console or the NuGet package manager UI in Visual Studio to search for and install the package.

```csharp
Install-Package OchoaLopes.ExprEngine
```

### Values Formats
ExprEngine supports a few formats as input parameters in expressions:

- Integer: Represented by appending an 'i' to the number. Example: "10i"
- Float: Represented by appending an 'f' to the number. Example: "3.14f"
- Double: Represented by appending an 'D' to the number. Example: "6.02D"
- Decimal: Represented by appending a 'd' to the number. Example: "100.0d"
- Dates: Represented by appending a 't' to the string. Example: " '2001-01-01't "

## Dates

ExprEngine provides support for date operations, making it flexible and adaptable to a variety of scenarios. It allows for adding or subtracting days from a date and comparing dates.

### Variable
To use variable you have to add a placeholder in your expression, then you can use a dictionary or an ordered list of values, this is available for ValidateExpression, EvaluateExpression and ComputeExpression.

```csharp
string expression = ":firstVariable * 10i <= :secondVariable";
```

#### Comparison

ExprEngine supports comparison of dates with operators like `==`, `!=`, `>`, `<`, `>=`, `<=`. For example, `:birthDate > :otherDate`.

Please note that the date format and operations depend on the `CultureInfo` passed to the `ExpressionService` constructor.

This will influence the following:

- Number format (decimal separator, thousand separator, etc.)
- Date format
- First day of the week
- Whether week can be split between two years (ISO 8601)
- Mathematical operations based on specific cultural norms

### Usage

1. Add a reference to the OchoaLopes.ExprEngine namespace:

```csharp
using OchoaLopes.ExprEngine;
```

2. Create an instance of `ExpressionService`:

```csharp
var expressionService = new ExpressionService();
```

3. Validate an expression:

```csharp
string expression = "(:x + 5i) > 10i";
var variables = new Dictionary<string, object>
{
    { "x", 7 }
};

bool isValid = expressionService.ValidateExpression(expression, variables);
```

4. Evaluate an expression:

```csharp
string expression = "(:x > :y) && (:z < :w)";
var values = new List<object> { 2, 4, 10, 5 }; // x=2, y=4, z=10, w=5

bool result = expressionService.EvaluateExpression(expression, values);
```

5. Compute an expression:

```csharp
string expression = "(:x + 5i) * (3.14f - :y)";
var variables = new Dictionary<string, object>
{
    { "x", 7 },
    { "y", 2.5 }
};

object result = expressionService.ComputeExpression(expression, variables);
```

### Additional Methods

In addition to the basic expression evaluation functionality, the ExprEngine library provides the following methods:

- `bool ValidateExpression(string expression, CultureInfo? cultureInfo = null)`: Validates the syntax and structure of an expression, considering the specified culture information.

- `bool ValidateExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null)`: Validates the syntax and structure of an expression with the help of a dictionary of variables and their values, considering the specified culture information.

- `bool ValidateExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null)`: Validates the syntax and structure of an expression with the help of a list of values for indexed placeholders, considering the specified culture information.

- `bool EvaluateExpression(string expression, CultureInfo? cultureInfo = null)`: Evaluates the expression and returns a boolean result, considering the specified culture information.

- `bool EvaluateExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null)`: Evaluates the expression and returns a boolean result with the help of a dictionary of variables and their values, considering the specified culture information.

- `bool EvaluateExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null)`: Evaluates the expression and returns a boolean result with the help of a list of values for indexed placeholders, considering the specified culture information.

- `object ComputeExpression(string expression, CultureInfo? cultureInfo = null)`: Evaluates and computes the expression, returning the result, considering the specified culture information.

- `object ComputeExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null)`: Evaluates and computes the expression, returning the result with the help of a dictionary of variables and their values, considering the specified culture information.

- `object ComputeExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null)`: Evaluates and computes the expression, returning the result with the help of a list of values for indexed placeholders, considering the specified culture information.

## 6. Limitations

While ExprEngine provides a range of expression evaluation capabilities, it does have some limitations:

- The library supports a subset of mathematical and logical operations, including addition, subtraction, multiplication, division, comparison, logical AND, and logical OR.

- The library only supports variables as input parameters. It does not provide facilities for defining custom functions or complex expressions.

- ExprEngine is designed for simple arithmetic and logical expressions and may not be suitable for complex symbolic or mathematical computations.

- The library may have performance limitations when dealing with large expressions or complex calculations. It is recommended to benchmark and profile your specific use cases to ensure satisfactory performance.

## 7. Feedback

We welcome your feedback and contributions to the ExprEngine library. If you have any questions, suggestions, or issues, please feel free to open an issue on the GitHub repository. We appreciate your feedback and strive to continuously improve the library.

## 8. License

The ExprEngine library is licensed under the [Apache License 2.0](https://opensource.org/licenses/Apache-2.0). You are free to use, modify, and distribute this library in accordance with the terms of the license. Please see the [LICENSE](https://github.com/your/repo/blob/main/LICENSE) file for more details.
