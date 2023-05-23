using OchoaLopes.ExprEngine.Expressions;

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
        public void NotEqualToTest_Doubles()
        {
            var expr = new NotEqual(new Literal(5.0m), new Literal(3.0m));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(true));

            expr = new NotEqual(new Literal(3.0m), new Literal(3.0m));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(false));
        }

        [Test]
        public void NotEqualToTest_Strings()
        {
            var expr = new NotEqual(new Literal("a"), new Literal("b"));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(true));

            expr = new NotEqual(new Literal("a"), new Literal("a"));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(false));
        }

        [Test]
        public void NotEqualToTest_Booleans()
        {
            var expr = new NotEqual(new Literal(true), new Literal(false));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(true));

            expr = new NotEqual(new Literal(true), new Literal(true));
            Assert.That((bool)expr.Evaluate(variables), Is.EqualTo(false));
        }
    }
}