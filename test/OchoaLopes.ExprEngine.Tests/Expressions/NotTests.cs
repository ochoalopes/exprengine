using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class NotTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void NotTest()
        {
            var expr = new Not(new Literal(true));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new Not(new Literal(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }
    }
}