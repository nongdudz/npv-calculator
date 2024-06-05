namespace NPVCalculator.Tests
{
    [TestFixture]
    public class NetPresentValueTests
    {
        [Test]
        public void Constructor_InvalidInitialInvestments_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => new NetPresentValue(-1000, 1, new List<decimal> { 200, 300, 400 });

            // Act & Assert
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Initial investment should be greater than zero. (Parameter 'initialInvestments')");
        }

        [Test]
        public void Constructor_InvalidInterestRate_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => new NetPresentValue(1000, -1, new List<decimal> { 200, 300, 400 });

            // Act & Assert
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Interest rate should be greater than zero. (Parameter 'interestRate')");
        }

        [Test]
        public void Constructor_NullOrEmptyCashFlows_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => new NetPresentValue(1000, 1, null);

            // Act & Assert
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Cash flows cannot be null or empty. (Parameter 'cashFlows')");

            // Arrange
            Action actEmpty = () => new NetPresentValue(1000, 1, new List<decimal>());

            // Act & Assert
            actEmpty.Should().Throw<ArgumentNullException>()
                    .WithMessage("Cash flows cannot be null or empty. (Parameter 'cashFlows')");
        }

        [Test]
        public void Calculate_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            var npv = new NetPresentValue(1000, 1, new List<decimal> { 200, 300, 400 });

            // Act
            var result = npv.Calculate();

            // Assert
            result.NPV.Should().Be(-119.66M);
            result.CashFlowStream.Should().BeEquivalentTo(new Dictionary<int, (decimal, decimal)>
            {
                { 1, (200M, 198.02M) },
                { 2, (300M, 294.09M) },
                { 3, (400M, 388.24M) }
            }, options => options.WithStrictOrdering());
        }

        [Test]
        public void CalculateWithRange_InvalidDiscountRateRange_ThrowsArgumentException()
        {
            // Arrange
            var npv = new NetPresentValue(1000, 1, new List<decimal> { 200, 300, 400 });

            // Act
            Action act = () => npv.CalculateWithRange(2, 1, 1);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Lower discount rate must be less than upper discount rate.");
        }

        [Test]
        public void CalculateWithRange_InvalidDiscountRateIncrement_ThrowsArgumentException()
        {
            // Arrange
            var npv = new NetPresentValue(1000, 1, new List<decimal> { 200, 300, 400 });

            // Act
            Action act = () => npv.CalculateWithRange(1, 2, 0);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Discount rate increment must be greater than zero.");
        }

        [Test]
        public void CalculateWithRange_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            var npv = new NetPresentValue(1000, 1, new List<decimal> { 200, 300, 400 });

            // Act
            var result = npv.CalculateWithRange(5, 15, 5);

            // Assert
            result.Should().BeEquivalentTo(new List<(decimal, decimal, Dictionary<int, (decimal, decimal)>)>
            {
                (5M, -191.88M, new Dictionary<int, (decimal, decimal)>
                {
                    { 1, (200M, 190.48M) },
                    { 2, (300M, 272.11M) },
                    { 3, (400M, 345.54M) }
                }),
                (10M, -269.72M, new Dictionary<int, (decimal, decimal)>
                {
                    { 1, (200M, 181.82M) },
                    { 2, (300M, 247.93M) },
                    { 3, (400M, 300.53M) }
                }),
                (15M, -336.24M, new Dictionary<int, (decimal, decimal)>
                {
                    { 1, (200M, 173.91M) },
                    { 2, (300M, 226.84M) },
                    { 3, (400M, 263.01M) }
                })
            }, options => options.WithStrictOrdering());
        }
    }
}