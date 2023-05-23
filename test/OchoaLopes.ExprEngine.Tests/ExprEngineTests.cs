namespace OchoaLopes.ExprEngine.Tests
{
    internal class ExprEngineTests
    {
        private ExprEngine _exprEngine;

        [SetUp]
        public void Setup()
        {
            _exprEngine = new ExprEngine();
        }

        [Test]
        public void ValidateExpressionWithVariables_WithValidAddOperandInput_ReturnsTrue()
        {
            // Arrange
            var expression = ":x + :y";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 } };

            // Act
            var result = _exprEngine.ValidateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithValidSubtractOperandInput_ReturnsTrue()
        {
            // Arrange
            var expression = ":x - :y";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 } };

            // Act
            var result = _exprEngine.ValidateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithInvalidExpression_ReturnsFalse()
        {
            // Arrange
            var expression = ":x + ";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 } };

            // Act
            var result = _exprEngine.ValidateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateExpressionWithVariables_WithMissingVariable_ReturnsFalse()
        {
            // Arrange
            var expression = ":x + :y";
            var variables = new Dictionary<string, object> { { ":x", 1 } };

            // Act
            var result = _exprEngine.ValidateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithValidInput_ReturnsExpectedValue()
        {
            // Arrange
            var expression = ":x == 1i && :y == 2i";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 } };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithInvalidInput_ThrowsException()
        {
            // Arrange
            var expression = ":x == 1i && :y == 2i";
            var variables = new Dictionary<string, object> { { ":x", 1 } };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithComplexInput_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x + :y) * :z == :result";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 }, { ":z", 3 }, { ":result", 9 } };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithComplexInputInvalid_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x + :y) * :z == :result";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 }, { ":z", 3 }, { ":result", 10 } };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void EvaluateExpressionWithVariables_WithComplexLogical_ReturnsExpectedValue()
        {
            // Arrange
            var expression = "(:x > :y) && (:z < :w) || (:result == :z)";
            var variables = new Dictionary<string, object> { { ":x", 1 }, { ":y", 2 }, { ":z", 3 }, { ":w", 4 }, { ":result", 3 } };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 1 },
                { ":y", 2 },
                { ":z", 3 },
                { ":w", 4 },
                { ":a", 5 },
                { ":b", 5 },
                { ":c", 6 },
                { ":d", 7 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 1 },
                { ":y", 2 },
                { ":z", 3 },
                { ":w", 4 },
                { ":a", 5 },
                { ":b", 5 },
                { ":c", 6 },
                { ":d", 7 }
            };

            // Act
            var result = _exprEngine.ValidateExpression(expression, variables);

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
                { ":x", 1 },
                { ":y", 2 },
                { ":z", 3 },
                { ":w", 4 },
                { ":a", 5 },
                { ":b", 5 },
                { ":c", 6 },
                { ":d", 7 }
            };

            // Act
            var result = _exprEngine.ValidateExpression(expression, variables);

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
            var result = _exprEngine.EvaluateExpression(expression, values);

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
            var result = _exprEngine.EvaluateExpression(expression, values);

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
            var result = _exprEngine.EvaluateExpression(expression, values);

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
            var result = _exprEngine.EvaluateExpression(expression, values);

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
                { ":x", 4 },
                { ":z", 2 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 4 },
                { ":z", 2 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 4 },
                { ":z", 2 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 4 },
                { ":z", 2 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":result", true }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 4 },
                { ":z", 2 }
            };

            // Act
            var result = (double)_exprEngine.ComputeExpression(expression, variables);

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
                { ":x", null }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 1 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 1 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", true }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

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
                { ":x", 1 },
                { ":z", 5 },
                { ":y", 2.5 }
            };

            // Act
            var result = _exprEngine.EvaluateExpression(expression, variables);

            // Assert
            Assert.IsTrue(result);
        }
    }
}