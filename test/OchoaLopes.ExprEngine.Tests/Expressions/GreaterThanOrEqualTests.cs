using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class GreaterThanOrEqualTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void GreaterThanOrEqualTest_Doubles()
        {
            var expr = new GreaterThanOrEqual(new Literal(5.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new GreaterThanOrEqual(new Literal(2.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new GreaterThanOrEqual(new Literal(3.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }

        [Test]
        public void GreaterThanOrEqualTest_Strings()
        {
            var expr = new GreaterThanOrEqual(new Literal("b"), new Literal("a"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new GreaterThanOrEqual(new Literal("a"), new Literal("b"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new GreaterThanOrEqual(new Literal("a"), new Literal("a"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }

        [Test]
        public void GreaterThanOrEqualTest_InvalidTypes()
        {
            var expr = new GreaterThanOrEqual(new Literal(true), new Literal(false));

            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}