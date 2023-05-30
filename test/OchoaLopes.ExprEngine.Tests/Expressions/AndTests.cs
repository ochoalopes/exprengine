using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

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
            var expr = new And(new LiteralBool(true), new LiteralBool(true));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new And(new LiteralBool(true), new LiteralBool(false));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}