using MathAPI.Controllers;
using MathAPI.Models;
using MathAPI.Repositories;
using MathAPITestProject.TestRepos;

namespace MathAPITestProject
{
    public class Tests
    {
        TestCalculationRepository _testCalcRepo;
        TestRelationRepository _testRelRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            _testCalcRepo = new TestCalculationRepository();
            _testRelRepo = new TestRelationRepository();
        }

        [Test]
        public void Calculate_ValidExpression_ReturnsExpectedResult()
        {
            // Arrange
            Calculation calc = new Calculation("(1.5 + 5)*3 + 4^5/2 - log(8;2)", _testCalcRepo, _testRelRepo);

            // Act
            calc.Calculate();

            // Assert
            Assert.That(calc.result, Is.EqualTo(528.5));
        }

        [Test]
        public void Constructor_InvalidRelationInParentheses_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("{} + 2", _testCalcRepo, _testRelRepo);
            });
        }

        [Test]
        public void Constructor_UnequalParenthesesCount_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("()(()", _testCalcRepo, _testRelRepo);
            });
        }

        [Test]
        public void Constructor_DoubleOperator_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("2++2", _testCalcRepo, _testRelRepo); 
            });
        }

        [Test]
        public void Constructor_NonNumericalInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("(22+2) + a", _testCalcRepo, _testRelRepo);
            });
        }

        [Test]
        public void Constructor_CalculationRefersToSelf_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("{1} + a", _testCalcRepo, _testRelRepo, 1);
            });
        }

        [Test]
        public void Calculate_DivisionByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                Calculation calc = new Calculation("5 / 0", _testCalcRepo, _testRelRepo);
                calc.Calculate();
            });
        }

        [Test]
        public void Constructor_EmptyInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Calculation calc = new Calculation("", _testCalcRepo, _testRelRepo);
            });
        }

        [Test]
        public void Calculate_InvalidLogarithmBase_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("log(8; -2)", _testCalcRepo, _testRelRepo);
                calc.Calculate();
            });
        }

        [Test]
        public void Calculate_NegativeNumbers_ReturnsExpectedResult()
        {
            Calculation calc = new Calculation("-5 + 3", _testCalcRepo, _testRelRepo);
            calc.Calculate();
            Assert.That(calc.result, Is.EqualTo(-2));
        }

        [Test]
        public void Calculate_ParenthesesAffectPriority_ReturnsExpectedResult()
        {
            Calculation calc = new Calculation("(2 + 3) * 4", _testCalcRepo, _testRelRepo);
            calc.Calculate();
            Assert.That(calc.result, Is.EqualTo(20));
        }

        [Test]
        public void Calculate_InvalidFunctionArguments_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Calculation calc = new Calculation("log(8)", _testCalcRepo, _testRelRepo);
                calc.Calculate();
            });
        }

    }
}