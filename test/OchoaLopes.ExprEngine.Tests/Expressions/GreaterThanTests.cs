using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class GreaterThanTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>
            {
                { "input", 150 }
            };
        }

        [Test]
        public void GreaterThanTest()
        {
            var expr = new GreaterThan(new Variable("input"), new Literal(100));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new GreaterThan(new Variable("input"), new Literal(200));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}