using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Tests.Expressions
{
    [TestFixture]
    internal class ModuloTests
    {
        private Dictionary<string, object> variables;

        [SetUp]
        public void SetUp()
        {
            variables = new Dictionary<string, object>();
        }

        [Test]
        public void ModuloTest_Doubles()
        {
            var expr = new Modulo(new LiteralDouble(5.0), new LiteralDouble(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(2.0));

            expr = new Modulo(new LiteralDouble(10.0), new LiteralDouble(2.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(0.0));
        }

        [Test]
        public void ModuloTest_DivideByZero()
        {
            var expr = new Modulo(new LiteralDouble(5.0), new LiteralDouble(0.0));
            Assert.Throws<DivideByZeroException>(() => expr.Evaluate(variables));
        }

        [Test]
        public void ModuloTest_InvalidTypes()
        {
            var expr = new Modulo(new LiteralString("5"), new LiteralString("3"));
            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}