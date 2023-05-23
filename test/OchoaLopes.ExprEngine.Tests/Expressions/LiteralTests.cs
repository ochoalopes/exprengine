using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class LiteralTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void LiteralTest()
        {
            var expr = new Literal(123);
            Assert.That(expr.Evaluate(variables), Is.EqualTo(123));

            expr = new Literal("test");
            Assert.That(expr.Evaluate(variables), Is.EqualTo("test"));

            expr = new Literal(null);
            Assert.That(expr.Evaluate(variables), Is.EqualTo(null));
        }
    }
}