using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

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
            var expr = new Add(new LiteralString("Hello "), new LiteralString("World!"));

            Assert.That(expr.Evaluate(variables), Is.EqualTo("Hello World!"));
        }

        [Test]
        public void AddTest_Doubles()
        {
            var expr = new Add(new LiteralDouble(3.0), new LiteralDouble(4.2));

            Assert.That(expr.Evaluate(variables), Is.EqualTo(7.2));
        }

        [Test]
        public void AddTest_InvalidTypes()
        {
            var expr = new Add(new LiteralBool(true), new LiteralBool(false));

            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}