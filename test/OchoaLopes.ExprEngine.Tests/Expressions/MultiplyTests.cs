using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class MultiplyTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void MultiplyTest_Doubles()
        {
            var expr = new Multiply(new LiteralDouble(5.0), new LiteralDouble(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(15.0));

            expr = new Multiply(new LiteralDouble(10.0), new LiteralDouble(2.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(20.0));
        }

        [Test]
        public void MultiplyTest_InvalidTypes()
        {
            var expr = new Multiply(new LiteralString("5"), new LiteralString("3"));
            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}