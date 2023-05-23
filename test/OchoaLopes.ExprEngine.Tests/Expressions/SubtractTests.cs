using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class SubtractTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void SubtractTests_Subtract()
        {
            var expr = new Subtract(new Literal(5.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(2.0));

            expr = new Subtract(new Literal(8.0), new Literal(12.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(-4.0));
        }

        [Test]
        public void MultiplyTest_InvalidTypes()
        {
            var expr = new Subtract(new Literal("5"), new Literal("3"));
            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}