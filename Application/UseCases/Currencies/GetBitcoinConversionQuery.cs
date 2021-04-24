using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Currencies
{
    public class GetBitcoinConversionQuery: IRequest<double>
    {
        private string Abbreviation { get;  }
        private double Value { get; }
        public GetBitcoinConversionQuery(string abbreviation, double value)
        {
            Abbreviation = abbreviation;
            Value = value;
        }
        
        public class Handler: IRequestHandler<GetBitcoinConversionQuery, double>
        {
            private readonly IBitcoinPriceService _bitcoinPriceService;

            public Handler(IBitcoinPriceService bitcoinPriceService)
            {
                _bitcoinPriceService = bitcoinPriceService;
            }
            
            public Task<double> Handle(GetBitcoinConversionQuery request, CancellationToken cancellationToken)
            {
                var conversionInfo = new BitcoinConversionInfo
                {
                    Amount = request.Value,
                    Abbreviation = request.Abbreviation
                };
                return _bitcoinPriceService.ConvertToBitcoin(conversionInfo, cancellationToken);
            }
        }
        
        public class Validator: AbstractValidator<GetBitcoinConversionQuery>
        {
            private readonly IMediator _mediator;

            public Validator(IMediator mediator)
            {
                _mediator = mediator;

                RuleFor(x => x.Abbreviation)
                    .NotEmpty()
                    .MustAsync(Exist)
                    .WithMessage("Abbreviation is not an allowed type");

                RuleFor(x => x.Value)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(Math.Pow(10, 6));
            }
            
            private async Task<bool> Exist(string abbreviation, CancellationToken cancellationToken)
            {
                if (abbreviation == null)
                {
                    return true;
                }
                
                var abbreviations =
                    await _mediator.Send(new GetAllCurrencyAbbreviationsQuery(), cancellationToken);

                return abbreviations.Contains(abbreviation.ToUpper());
            }
        }
    }
}