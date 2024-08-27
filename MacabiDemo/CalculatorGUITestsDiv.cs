using MacabiDemo.NessAutomationClient;

namespace MacabiDemo
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class CalculatorGUITestsDiv
    {
        private CalculatorGUI calculatorGUI;

        [OneTimeSetUp]
        public void Setup()
        {
            this.calculatorGUI = new CalculatorGUI();
        }

        [TestCase(10, 2, 5)]
        [TestCase(-10, 2, -5)]
        [TestCase(10, -2, -5)]
        [TestCase(-10, -2, 5)]
        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(-1, 1, -1)]
        [TestCase(1, -1, -1)]
        [TestCase(0, -1, 0)]
        [TestCase(100, 10, 10)]
        [TestCase(100, -10, -10)]
        [TestCase(-100, 10, -10)]
        [TestCase(-100, -10, 10)]
        [TestCase(int.MaxValue, 1, int.MaxValue)]
        [TestCase(int.MinValue, 1, int.MinValue)]
        [TestCase(int.MaxValue, -1, -int.MaxValue)]
        [TestCase(int.MaxValue, int.MaxValue, 1)] // Division of same large numbers is 1
        [TestCase(int.MinValue, int.MinValue, 1)] // Division of same large negative numbers is 1
        public void div_validinputs_GUI(int num1, int num2, double expectedsum)
        {
            this.calculatorGUI.FirstNumber = num1;
            this.calculatorGUI.SecondNumber = num2;
            this.calculatorGUI.SelectOperation("/");
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



