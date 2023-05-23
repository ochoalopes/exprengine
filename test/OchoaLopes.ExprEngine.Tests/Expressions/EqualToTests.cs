using OchoaLopes.ExprEngine.Expressions;

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
            var expr = new Equal(new Literal(150), new Variable("input"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            expr = new Equal(new Literal(100), new Variable("input"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}