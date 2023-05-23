using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class VariableTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>
            {
                { "testVar", 123 },
                { "stringVar", "hello" },
                { "nullVar", null }
            };
        }

        [Test]
        public void VariableTest()
        {
            var expr = new Variable("testVar");
            Assert.That(expr.Evaluate(variables), Is.EqualTo(123));

            expr = new Variable("stringVar");
            Assert.That(expr.Evaluate(variables), Is.EqualTo("hello"));

            expr = new Variable("nullVar");
            Assert.That(expr.Evaluate(variables), Is.EqualTo(null));
        }

        [Test]
        public void VariableTest_NotFound()
        {
            var expr = new Variable("nonExistentVar");

            Assert.Throws<KeyNotFoundException>(() => expr.Evaluate(variables));
        }
    }
}