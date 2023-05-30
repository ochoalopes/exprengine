using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

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
            var expr = new Not(new LiteralBool(true));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));

            expr = new Not(new LiteralBool(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));
        }
    }
}