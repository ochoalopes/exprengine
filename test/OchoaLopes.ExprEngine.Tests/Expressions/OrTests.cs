using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class OrTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void OrTest()
        {
            var expr = new Or(new Literal(true), new Literal(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new Or(new Literal(false), new Literal(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}