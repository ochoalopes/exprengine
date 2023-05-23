using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class IsNotNullTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void IsNotNullTest()
        {
            var expr = new IsNotNull(new Variable("input"));
            variables["input"] = 150;
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            variables["input"] = null;
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}