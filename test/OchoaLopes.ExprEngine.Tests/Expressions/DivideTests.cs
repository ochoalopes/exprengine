using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class DivideTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void DivideTest_Doubles()
        {
            var expr = new Divide(new Literal(10.0), new Literal(2.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(5.0));
        }

        [Test]
        public void DivideTest_DivideByZero()
        {
            var expr = new Divide(new Literal(10.0), new Literal(0.0));
            Assert.Throws<DivideByZeroException>(() => expr.Evaluate(variables));
        }

        [Test]
        public void DivideTest_InvalidTypes()
        {
            var expr = new Divide(new Literal("10"), new Literal("2"));
            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}