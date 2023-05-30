using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class NotEqualToTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void NotEqualToTest_Decimal()
        {
            var expr = new NotEqual(new LiteralDecimal(5.0m), new LiteralDecimal(3.0m));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(true));

            expr = new NotEqual(new LiteralDecimal(3.0m), new LiteralDecimal(3.0m));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(false));
        }

        [Test]
        public void NotEqualToTest_Strings()
        {
            var expr = new NotEqual(new LiteralString("a"), new LiteralString("b"));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(true));

            expr = new NotEqual(new LiteralString("a"), new LiteralString("a"));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(false));
        }

        [Test]
        public void NotEqualToTest_Booleans()
        {
            var expr = new NotEqual(new LiteralBool(true), new LiteralBool(false));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(true));

            expr = new NotEqual(new LiteralBool(true), new LiteralBool(true));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}