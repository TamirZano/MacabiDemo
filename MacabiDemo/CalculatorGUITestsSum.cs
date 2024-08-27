using MacabiDemo.NessAutomationClient;

namespace MacabiDemo
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class CalculatorGUITestsSum
    {
        private CalculatorGUI calculatorGUI;

        [OneTimeSetUp]
        public void Setup()
        {
            this.calculatorGUI = new CalculatorGUI();
        }

        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        public void add_validinputs_GUI(int num1, int num2, double expectedsum)
        {
            this.calculatorGUI.FirstNumber = num1;
            this.calculatorGUI.SecondNumber = num2;
            this.calculatorGUI.SelectOperation("+");
            this.calculatorGUI.Calculate();
            double results = this.calculatorGUI.Result;
            this.calculatorGUI.ValidateResults(expectedsum);

            Assert.That(results, Is.EqualTo(expectedsum));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this.calculatorGUI.CloseSession();
        }

    }
}



