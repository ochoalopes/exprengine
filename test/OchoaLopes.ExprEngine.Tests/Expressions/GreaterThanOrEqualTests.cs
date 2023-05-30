using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

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
            var expr = new GreaterThanOrEqual(new LiteralDouble(5.0), new LiteralDouble(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new GreaterThanOrEqual(new LiteralDouble(2.0), new LiteralDouble(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new GreaterThanOrEqual(new LiteralDouble(3.0), new LiteralDouble(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }

        [Test]
        public void GreaterThanOrEqualTest_Strings()
        {
            var expr = new GreaterThanOrEqual(new LiteralChar('b'), new LiteralChar('a'));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new GreaterThanOrEqual(new LiteralChar('a'), new LiteralChar('b'));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new GreaterThanOrEqual(new LiteralChar('a'), new LiteralChar('a'));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }

        [Test]
        public void GreaterThanOrEqualTest_InvalidTypes()
        {
            var expr = new GreaterThanOrEqual(new LiteralBool(true), new LiteralBool(false));

            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}