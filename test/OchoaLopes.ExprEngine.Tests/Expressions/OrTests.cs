using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

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
            var expr = new Or(new LiteralBool(true), new LiteralBool(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new Or(new LiteralBool(false), new LiteralBool(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}