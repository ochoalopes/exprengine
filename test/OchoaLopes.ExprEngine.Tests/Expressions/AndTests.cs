using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class AndTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void AndTest()
        {
            var expr = new And(new Literal(true), new Literal(true));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new And(new Literal(true), new Literal(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}