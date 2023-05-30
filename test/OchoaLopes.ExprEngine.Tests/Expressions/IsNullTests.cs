using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class IsNullTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void IsNullTest()
        {
            var expr = new IsNull(new Variable("input"));
            variables["input"] = null;
            Assert.That(expr.Evaluate(variables), Is.EqualTo(true));

            variables["input"] = 150;
            Assert.That(expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}