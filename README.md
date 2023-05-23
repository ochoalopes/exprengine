# ExprEngine

ExprEngine is a lightweight expression evaluation library that allows you to validate, evaluate, and compute mathematical expressions with support for variables.

## Getting Started

### Installation

The ExprEngine library can be installed via NuGet. Use the package manager console or the NuGet package manager UI in Visual Studio to search for and install the package.

```csharp
Install-Package ExprEngine

### Usage

1. Add a reference to the ExprEngine namespace:

```csharp
using OchoaLopes.ExprEngine;

2. Create an instance of `ExprEngine`:

```csharp
var exprEngine = new ExprEngine();

3. Validate an expression:

```csharp
string expression = "(:x + 5i) > 10i";
var variables = new Dictionary<string, object>
{
    { ":x", 7 }
};

bool isValid = exprEngine.ValidateExpression(expression, variables);

4. Evaluate an expression:

```csharp

string expression = "(:x + 5i) > 10i";
var variables = new Dictionary<string, object>
{
    { ":x", 7 }
};

bool result = exprEngine.EvaluateExpression(expression, variables);

5. Compute an expression:

```csharp
string expression = "(:x + 5i) * (3.14f - :y)";
var variables = new Dictionary<string, object>
{
    { ":x", 7 },
    { ":y", 2.5 }
};

object result = exprEngine.ComputeExpression(expression, variables);

### Additional Methods

In addition to the basic expression evaluation functionality, the ExprEngine library provides the following methods:

- `bool ValidateExpression(string expression, IDictionary<string, object>? variables = null)`: Validates the syntax and structure of an expression. Optionally, you can provide a dictionary of variables and their values to validate expressions with variables.

- `bool ValidateExpression(string expression, IList<object>? values = null)`: Validates the syntax and structure of an expression. Optionally, you can provide a list of values to validate expressions with indexed placeholders.

- `bool EvaluateExpression(string expression, IDictionary<string, object>? variables = null)`: Evaluates the expression and returns a boolean result. Optionally, you can provide a dictionary of variables and their values to evaluate expressions with variables.

- `bool EvaluateExpression(string expression, IList<object>? values = null)`: Evaluates the expression and returns a boolean result. Optionally, you can provide a list of values to evaluate expressions with indexed placeholders.

- `object ComputeExpression(string expression, IDictionary<string, object>? variables = null)`: Evaluates and computes the expression, returning the result. Optionally, you can provide a dictionary of variables and their values to compute expressions with variables.

- `object ComputeExpression(string expression, IList<object>? values = null)`: Evaluates and computes the expression, returning the result. Optionally, you can provide a list of values to compute expressions with indexed placeholders.

## 6. Limitations

While ExprEngine provides a range of expression evaluation capabilities, it does have some limitations:

- The library supports a subset of mathematical and logical operations, including addition, subtraction, multiplication, division, comparison, logical AND, and logical OR.

- The library only supports variables as input parameters. It does not provide facilities for defining custom functions or complex expressions.

- ExprEngine is designed for simple arithmetic and logical expressions and may not be suitable for complex symbolic or mathematical computations.

- The library may have performance limitations when dealing with large expressions or complex calculations. It is recommended to benchmark and profile your specific use cases to ensure satisfactory performance.

## 7. Feedback

We welcome your feedback and contributions to the ExprEngine library. If you have any questions, suggestions, or issues, please feel free to open an issue on the GitHub repository. We appreciate your feedback and strive to continuously improve the library.

## 8. License

The ExprEngine library is licensed under the [MIT License](https://opensource.org/licenses/MIT). You are free to use, modify, and distribute this library in accordance with the terms of the license. Please see the [LICENSE](https://github.com/your/repo/blob/main/LICENSE) file for more details.

