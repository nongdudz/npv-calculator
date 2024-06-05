namespace NPVCalculator.Tests.Services
{
    public class CalculatorServiceTests
    {
        private CalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _calculatorService = new CalculatorService();
        }

        [Test]
        public async Task CalculateNPVAsync_ValidRequest_ReturnsExpectedResult()
        {
            // Arrange
            var expectedOutput = GenerateExpectedNpvOutput();

            var npvRequest = new NPVRequest
            {
                InitialInvestments = 1000,
                InterestRate = 1m,
                CashFlows = new List<decimal> { 200, 300, 400 }
            };

            // Act
            var result = await _calculatorService.CalculateNPVAsync(npvRequest);

            // Assert
            result.Should().NotBeNull();
            result.CalculatedNPV.Should().Be(expectedOutput.CalculatedNPV);
            result.CashFlowSeries.Should().BeEquivalentTo(expectedOutput.CashFlowSeries, options => options.WithStrictOrdering());
        }

        [Test]
        public void CalculateNPVAsync_InvalidInitialInvestments_ThrowsArgumentException()
        {
            // Arrange
            var npvRequest = new NPVRequest
            {
                InitialInvestments = -1000,
                InterestRate = 1m,
                CashFlows = new List<decimal> { 200, 300, 400 }
            };

            // Act
            Func<Task> act = () => _calculatorService.CalculateNPVAsync(npvRequest);

            // Assert
            act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Initial investments must be greater than zero. (Parameter 'InitialInvestments')");
        }

        [Test]
        public void CalculateNPVAsync_NullCashFlows_ThrowsArgumentException()
        {
            // Arrange
            var npvRequest = new NPVRequest
            {
                InitialInvestments = 1000,
                InterestRate = 1m,
                CashFlows = null
            };

            // Act
            Func<Task> act = () => _calculatorService.CalculateNPVAsync(npvRequest);

            // Assert
            act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Cash flows cannot be null or empty. (Parameter 'CashFlows')");
        }

        [Test]
        public void CalculateNPVWithDiscountRateRangeAsync_InvalidDiscountRateRange_ThrowsArgumentException()
        {
            // Arrange
            var npvRequest = new NPVRequest
            {
                InitialInvestments = 1000,
                InterestRate = 1m,
                CashFlows = new List<decimal> { 200, 300, 400 },
                DiscountRateRange = new DiscountRateRange
                {
                    LowerDiscountRate = 2m,
                    UpperDiscountRate = 1m,
                    DiscountRateIncrement = 1m
                }
            };

            // Act
            Func<Task> act = () => _calculatorService.CalculateNPVWithDiscountRateRangeAsync(npvRequest);

            // Assert
            act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Lower discount rate must be less than upper discount rate. (Parameter 'LowerDiscountRate')");
        }

        [Test]
        public async Task CalculateNPVWithDiscountRateRangeAsync_ValidRequest_ReturnsExpectedResult()
        {
            // Arrange
            var expectedOutput = GenerateExpectedNPVRangeOutput();

            var npvRequest = new NPVRequest
            {
                InitialInvestments = 1000,
                InterestRate = 1m,
                CashFlows = new List<decimal> { 200, 300 },
                DiscountRateRange = new DiscountRateRange
                {
                    LowerDiscountRate = 5m,
                    UpperDiscountRate = 15m,
                    DiscountRateIncrement = 2.5m
                }
            };

            // Act
            var result = await _calculatorService.CalculateNPVWithDiscountRateRangeAsync(npvRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
        }

        private NPVResponse GenerateExpectedNpvOutput()
        {
            return new NPVResponse
            {
                CalculatedNPV = -119.66M,
                CashFlowSeries = new List<CashFlowSeries>
                {
                    new CashFlowSeries
                    {
                        CashFlow = -1000M,
                        Period = 0,
                        PresentValue =  -1000M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 200M,
                        Period = 1,
                        PresentValue =  198.02M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 300M,
                        Period = 2,
                        PresentValue =  294.09M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 400M,
                        Period = 3,
                        PresentValue =  388.24M
                    }
                }
            };
        }

        private List<NPVRangeResponse> GenerateExpectedNPVRangeOutput()
        {
            return new List<NPVRangeResponse> {
            new NPVRangeResponse
            {
                CalculatedNPV = -537.41M,
                Rate = 5.00M
,
                CashFlowSeries = new List<CashFlowSeries>
                {
                    new CashFlowSeries
                    {
                        CashFlow = -1000M,
                        Period = 0,
                       PresentValue = -1000M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 200M,
                        Period = 1,
                       PresentValue = 190.48M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 300M,
                        Period = 2,
                       PresentValue = 272.11M
                    }
                }
            },
            new NPVRangeResponse
            {
                CalculatedNPV = -554.35M,
                Rate =7.500M
,
                CashFlowSeries = new List<CashFlowSeries>
                {
                    new CashFlowSeries
                    {
                        CashFlow = -1000M,
                        Period = 0,
                       PresentValue = -1000M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 200M,
                        Period = 1,
                       PresentValue = 186.05M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 300M,
                        Period = 2,
                       PresentValue =259.60M
                    }
                }
            },
            new NPVRangeResponse
            {
                CalculatedNPV = -570.25M,
                Rate = 10.000M
,
                CashFlowSeries = new List<CashFlowSeries>
                {
                    new CashFlowSeries
                    {
                        CashFlow = -1000M,
                        Period = 0,
                       PresentValue = -1000M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 200M,
                        Period = 1,
                       PresentValue = 181.82M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 300M,
                        Period = 2,
                       PresentValue = 247.93M
                    }
                }
            },
            new NPVRangeResponse
            {
                CalculatedNPV = -585.19M,
                Rate = 12.500M
,
                CashFlowSeries = new List<CashFlowSeries>
                {
                    new CashFlowSeries
                    {
                        CashFlow = -1000M,
                        Period = 0,
                       PresentValue = -1000M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 200M,
                        Period = 1,
                       PresentValue =177.78M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 300M,
                        Period = 2,
                       PresentValue = 237.04M
                    }
                }
            },
            new NPVRangeResponse
            {
                CalculatedNPV = -599.24M,
                Rate = 15.000M
,
                CashFlowSeries = new List<CashFlowSeries>
                {
                    new CashFlowSeries
                    {
                        CashFlow = -1000M,
                        Period = 0,
                       PresentValue = -1000M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 200M,
                        Period = 1,
                       PresentValue =173.91M
                    },
                    new CashFlowSeries
                    {
                        CashFlow = 300M,
                        Period = 2,
                       PresentValue = 226.84M
                    }
                }
            }
            };
        }
    }
}