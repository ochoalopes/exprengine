using OchoaLopes.ExprEngine.Expressions;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class AddTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void AddTest_Strings()
        {
            var expr = new Add(new Literal("Hello "), new Literal("World!"));
            Assert.That(expr.Evaluate(variables), Is.EqualTo("Hello World!"));
        }

        [Test]
        public void AddTest_Doubles()
        {
            var expr = new Add(new Literal(3.0), new Literal(4.2));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(7.2));
        }

        [Test]
        public void AddTest_InvalidTypes()
        {
            var expr = new Add(new Literal(true), new Literal(false));

            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}