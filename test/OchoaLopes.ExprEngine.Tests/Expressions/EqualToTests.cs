using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class EqualToTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>
            {
                { "input", 150 }
            };
        }

        [Test]
        public void EqualToTest()
        {
            var expr = new Equal(new LiteralInteger(150), new Variable("input"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new Equal(new LiteralInteger(100), new Variable("input"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}