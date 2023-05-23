using OchoaLopes.ExprEngine.Expressions;

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
            var expr = new Modulo(new Literal(5.0), new Literal(3.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(2.0));

            expr = new Modulo(new Literal(10.0), new Literal(2.0));
            Assert.That(expr.Evaluate(variables), Is.EqualTo(0.0));
        }

        [Test]
        public void ModuloTest_DivideByZero()
        {
            var expr = new Modulo(new Literal(5.0), new Literal(0.0));
            Assert.Throws<DivideByZeroException>(() => expr.Evaluate(variables));
        }

        [Test]
        public void ModuloTest_InvalidTypes()
        {
            var expr = new Modulo(new Literal("5"), new Literal("3"));
            Assert.Throws<InvalidOperationException>(() => expr.Evaluate(variables));
        }
    }
}