using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class LiteralTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void LiteralTest()
        {
            var exprInt = new LiteralInteger(123);
            Assert.That(exprInt.Evaluate(variables), Is.EqualTo(123));

            var exprString = new LiteralString("test");
            Assert.That(exprString.Evaluate(variables), Is.EqualTo("test"));

            var exprNull = new LiteralNull(null);
            Assert.That(exprNull.Evaluate(variables), Is.EqualTo(null));
        }
    }
}