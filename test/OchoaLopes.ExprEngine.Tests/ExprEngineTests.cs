using System.Globalization;

namespace OchoaLopes.ExprEngine.Tests
{
    internal class ExprEngineTests
    {
        private ExpressionService _expressionService;

        [SetUp]
        public void Setup()
        {
            _expressionService = new ExpressionService(CultureInfo.InvariantCulture);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithValidAddOperandInput_ReturnsTrue()
        {
            // Arrange
            var expression = ":x + :y";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 } };

            // Act
            var result = _expressionService.ValidateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithValidSubtractOperandInput_ReturnsTrue()
        {
            // Arrange
            var expression = ":x - :y";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 } };

            // Act
            var result = _expressionService.ValidateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithInvalidExpression_ReturnsFalse()
        {
            // Arrange
            var expression = ":x + ";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 } };

            // Act
            var result = _expressionService.ValidateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithMissingVariable_ReturnsFalse()
        {
            // Arrange
            var expression = ":x + :y";
            var variables = new Dictionary<string, object> { { "x", 1 } };

            // Act
            var result = _expressionService.ValidateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithValidInput_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":x == 1i && :y == 2i";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithInvalidInput_ThrowsException()
        {
            // Arrange
            var expression = ":x == 1i && :y == 2i";
            var variables = new Dictionary<string, object> { { "x", 1 } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithComplexInput_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x + :y) * :z == :result";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 }, { "z", 3 }, { "result", 9 } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithComplexInputInvalid_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x + :y) * :z == :result";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 }, { "z", 3 }, { "result", 10 } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithComplexLogical_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x > :y) && (:z < :w) || (:result == :z)";
            var variables = new Dictionary<string, object> { { "x", 1 }, { "y", 2 }, { "z", 3 }, { "w", 4 }, { "result", 3 } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithNestedLogical_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "((:x > :y) && (:z < :w)) || ((:a == :b) && (:c != :d))";
            var variables = new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
                { "w", 4 },
                { "a", 5 },
                { "b", 5 },
                { "c", 6 },
                { "d", 7 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithNestedExpression_ReturnsTrue()
        {
            // Arrange
            var expression = "((:x > :y) && (:z < :w)) || ((:a == :b) && (:c != :d))";
            var variables = new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
                { "w", 4 },
                { "a", 5 },
                { "b", 5 },
                { "c", 6 },
                { "d", 7 }
            };

            // Act
            var result = _expressionService.ValidateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithInvalidNestedExpression_ReturnsFalse()
        {
            // Arrange
            var expression = "((:x > :y) && (:z < :w) && || ((:a == :b) && (:c != :d))"; // Syntax error
            var variables = new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
                { "w", 4 },
                { "a", 5 },
                { "b", 5 },
                { "c", 6 },
                { "d", 7 }
            };

            // Act
            var result = _expressionService.ValidateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithOrderedValues_WithValidExpressionAndOrderedValues_ReturnsExpectedResult()
        {
            // Arrange
            var expression = "(:x > :y) && (:z < :w)";
            var values = new List<object> { 4, 2, 5, 10 }; // x=4, y=2, z=5, w=10

            // Act
            var result = _expressionService.EvaluateExpression(expression, values);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithOrderedValues_WithInvalidOrderedValues_ReturnsFalse()
        {
            // Arrange
            var expression = "(:x > :y) && (:z < :w)";
            var values = new List<object> { 2, 4, 10, 5 }; // x=2, y=4, z=10, w=5

            // Act
            var result = _expressionService.EvaluateExpression(expression, values);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithOrderedValues_WithMissingValues_ReturnsFalse()
        {
            // Arrange
            var expression = "(:x > :y) && (:z < :w)";
            var values = new List<object> { 4, 2 }; // Missing values for z and w

            // Act
            var result = _expressionService.EvaluateExpression(expression, values);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithOrderedValues_WithInvalidExpressionSyntax_ReturnsFalse()
        {
            // Arrange
            var expression = "((:x > :y) && (:z < :w) || ((:a == :b) && (:c != :d))"; // Syntax error
            var values = new List<object> { 4, 2, 5, 10, 1, 1, 2, 2 };

            // Act
            var result = _expressionService.EvaluateExpression(expression, values);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_AddOperandValue_ReturnsTrue()
        {
            // Arrange
            var expression = "((:x + 4i) + :z) == 10i";
            var variables = new Dictionary<string, object>
            {
                { "x", 4 },
                { "z", 2 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_AddOperandValue_ReturnsFalse()
        {
            // Arrange
            var expression = "((:x + 4i) + :z) == 9i";
            var variables = new Dictionary<string, object>
            {
                { "x", 4 },
                { "z", 2 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_SubtractOperandAndUnaryMinusValue_ReturnsTrue()
        {
            // Arrange
            var expression = "((:x + -4i) + :z) == 2i";
            var variables = new Dictionary<string, object>
            {
                { "x", 4 },
                { "z", 2 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_MultiplyOperandAndUnaryMinusValue_ReturnsTrue()
        {
            // Arrange
            var expression = "(:x + 1i) * (5i - :z) <= 15i";
            var variables = new Dictionary<string, object>
            {
                { "x", 4 },
                { "z", 2 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_BooleanOperandAndUnaryMinusValue_ReturnsTrue()
        {
            // Arrange
            var expression = ":result";
            var variables = new Dictionary<string, object>
            {
                { "result", true }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ComputeExpressionWithVariables_MultiplyOperandAndUnaryMinusValue_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x + 1D) * (5i - :z) - 5f";
            var variables = new Dictionary<string, object>
            {
                { "x", 4 },
                { "z", 2 }
            };

            // Act
            var result = (double)_expressionService.ComputeExpression(expression, variables);

            // Assert
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void EvaluateExpressionWithVariables_IsNullOperandValue_ReturnsTrue()
        {
            // Arrange
            var expression = ":x == null";
            var variables = new Dictionary<string, object>
            {
                { "x", null }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_IsNotNullOperandValue_ReturnsTrue()
        {
            // Arrange
            var expression = ":x != null";
            var variables = new Dictionary<string, object>
            {
                { "x", 1 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_NotOperator_ReturnsTrue()
        {
            // Arrange
            var expression = "!(:x == 1i)";
            var variables = new Dictionary<string, object>
            {
                { "x", 1 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_NotOperatorWithBoolean_ReturnsTrue()
        {
            // Arrange
            var expression = "!:x";
            var variables = new Dictionary<string, object>
            {
                { "x", true }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_ValidateComplexComparasion_ReturnsTrue()
        {
            // Arrange
            var expression = "((:x + 5i) * :z) > (:y + 10d)";
            var variables = new Dictionary<string, object>
            {
                { "x", 1 },
                { "z", 5 },
                { "y", 2.5 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpression_SimpleComparison_ReturnsTrue()
        {
            // Arrange
            var expression = "(10i * 5i) > (1i * 6i)";

            // Act
            var result = _expressionService.ValidateExpression(expression);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpression_SimpleComparison_ReturnsTrue()
        {
            // Arrange
            var expression = "(10i * 5i) > (1i * 6i)";

            // Act
            var result = _expressionService.EvaluateExpression(expression);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ComputeExpression_SimpleMath_ReturnsTrue()
        {
            // Arrange
            var expression = "10i * 10d";

            // Act
            var result = (double)_expressionService.ComputeExpression(expression);

            // Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void EvaluateExpression_StringComparison_ReturnsTrue()
        {
            // Arrange
            var expression = ":input == 'test'";
            var variables = new Dictionary<string, object>
            {
                { "input", "test"}
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpression_DecimalComparison_ReturnsTrue()
        {
            // Arrange
            var expression = ":input > 1.5d";
            var variables = new Dictionary<string, object>
            {
                { "input", 1.6 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpression_DoubleComparisonBrazilianFormat_ReturnsTrue()
        {
            // Arrange
            var expression = ":input > 1,5D";
            var variables = new Dictionary<string, object>
            {
                { "input", 1.6 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables, new CultureInfo("pt-BR"));

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpression_FloatComparisonFrenchFormat_ReturnsTrue()
        {
            // Arrange
            var expression = ":input > 1,5D";
            var variables = new Dictionary<string, object>
            {
                { "input", 1.6 }
            };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables, new CultureInfo("fr-FR"));

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithValidDateInput_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate == '2001-01-01't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithValidDateFormat_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate == '01/01/2001't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };
            var cultureInfo = new CultureInfo("pt-BR"); // format: dd/MM/yyyy

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables, cultureInfo);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithInvalidDateInput_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate == '2001-01-02't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithDateComparison_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate < '2001-02-01't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithDateComparisonFrenchFormat_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate < '01/02/2001't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };
            var cultureInfo = new CultureInfo("fr-FR"); // format: dd/MM/yyyy

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables, cultureInfo);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithInvalidDateComparison_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate > '2001-02-01't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_AddDaysToDate_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate + 1i == '2001-01-02't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 1) } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_SubtractDaysToDate_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":birthDate - 1i == '2001-01-02't";
            var variables = new Dictionary<string, object> { { "birthDate", new DateTime(2001, 1, 3) } };

            // Act
            var result = _expressionService.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }
    }
}