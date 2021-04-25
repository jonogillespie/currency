using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Application.UseCases.Currencies;
using MediatR;
using Moq;
using Xunit;

namespace ApplicationTests.UseCases.Currencies
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GetBitcoinConversionQueryTests
    {
        public class HandlerTests
        {
            [Fact]
            public async Task Query_Returns_CorrectValue()
            {
                var mock = new Mock<IBitcoinPriceService>();
                const double convertedValue = 5.12334;

                mock.Setup(x => x.ConvertToBitcoin(It.IsAny<BitcoinConversionInfo>(),
                        CancellationToken.None))
                    .Returns(() => Task.Run(() => convertedValue));
                var handler = new GetBitcoinConversionQuery.Handler(mock.Object);

                var res = await handler.Handle(new GetBitcoinConversionQuery("GBP",
                        123),
                    CancellationToken.None);

                Assert.Equal(convertedValue, res);
            }
        }

        public class ValidatorTests
        {
            private readonly GetBitcoinConversionQuery.Validator _validator;

            public ValidatorTests()
            {
                var mockMediator = new Mock<IMediator>();
                mockMediator
                    .Setup(x => x.Send(It.IsAny<IRequest<List<string>>>(), CancellationToken.None))
                    .Returns(() => Task.Run(() => new List<string> {"GBP", "USD"}));
                
                _validator = new GetBitcoinConversionQuery.Validator(mockMediator.Object);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(999999)]
            [InlineData(1000000)]
            public async Task Validate_ValueBetween0And1000000_IsValid(double value)
            {
                var query = new GetBitcoinConversionQuery("GBP", value);
                var res = await _validator.ValidateAsync(query);
                Assert.True(res.IsValid);
            }

            [Fact]
            public async Task Validate_ValueNegative_HasErrors()
            {
                const double value = -0.1;
                var query = new GetBitcoinConversionQuery("GBP", value);
                var res = await _validator.ValidateAsync(query);
                Assert.False(res.IsValid);
                Assert.Equal("Value", res.Errors.First().PropertyName);
            }
            
            [Fact]
            public async Task Validate_ValueGreaterThan_1000000_HasErrors()
            {
                const double value = 1000000.1;
                var query = new GetBitcoinConversionQuery("GBP", value);
                var res = await _validator.ValidateAsync(query);
                Assert.False(res.IsValid);
                Assert.Equal("Value", res.Errors.First().PropertyName);
            }

            [Fact]
            public async Task Validate_AbbreviationNull_HasErrors()
            {
                const int value = 100;
                var query = new GetBitcoinConversionQuery(null, value);
                var res = await _validator.ValidateAsync(query);
                Assert.False(res.IsValid);
                Assert.Equal("Abbreviation", res.Errors.First().PropertyName);
            }
            
            [Fact]
            public async Task Validate_AbbreviationEmpty_HasErrors()
            {
                const int value = 100;
                var query = new GetBitcoinConversionQuery(" ", value);
                var res = await _validator.ValidateAsync(query);
                Assert.False(res.IsValid);
                Assert.Equal("Abbreviation", res.Errors.First().PropertyName);
            }
            
            [Fact]
            public async Task Validate_AbbreviationNotExists_HasErrors()
            {
                const int value = 100;
                var query = new GetBitcoinConversionQuery("123", value);
                var res = await _validator.ValidateAsync(query);
                Assert.False(res.IsValid);
                Assert.Equal("Abbreviation", res.Errors.First().PropertyName);
            }
        }
    }
}