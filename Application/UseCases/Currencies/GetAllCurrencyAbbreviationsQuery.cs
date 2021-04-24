using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Application.Interfaces.Internal;
using MediatR;

namespace Application.UseCases.Currencies
{
    public class GetAllCurrencyAbbreviationsQuery: IRequest<List<string>>, ICacheable
    {
        public class Handler: IRequestHandler<GetAllCurrencyAbbreviationsQuery, List<string>>
        {
            private readonly IBitcoinPriceService _bitcoinPriceService;

            public Handler(IBitcoinPriceService bitcoinPriceService)
            {
                _bitcoinPriceService = bitcoinPriceService;
            }
            
            public async Task<List<string>> Handle(GetAllCurrencyAbbreviationsQuery request, 
                CancellationToken cancellationToken)
            {
                var response =
                    await _bitcoinPriceService.GetCurrentPrices(cancellationToken);

                return response
                    .OrderBy(x => x.Key)
                    .Select(x => x.Key)
                    .ToList();
            }
        }
    }
}