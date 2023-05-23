using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class LessThanOrEqualTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void LessThanOrEqualTest_Doubles()
        {
            var expr = new LessThanOrEqual(new Literal(2.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new LessThanOrEqual(new Literal(5.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new LessThanOrEqual(new Literal(3.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }

        [Test]
        public void LessThanOrEqualTest_Strings()
        {
            var expr = new LessThanOrEqual(new Literal("a"), new Literal("b"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new LessThanOrEqual(new Literal("b"), new Literal("a"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new LessThanOrEqual(new Literal("a"), new Literal("a"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }

        [Test]
        public void LessThanOrEqualTest_InvalidTypes()
        {
            var expr = new LessThanOrEqual(new Literal(true), new Literal(false));

            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}