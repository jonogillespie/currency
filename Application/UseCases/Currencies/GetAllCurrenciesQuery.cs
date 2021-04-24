using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.External;
using Application.UseCases.Currencies.Dto;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Currencies
{
    public class GetAllCurrenciesQuery: IRequest<CurrencyVm>
    {
        public class Handler : IRequestHandler<GetAllCurrenciesQuery, CurrencyVm>
        {
            private readonly IBitcoinPriceService _bitcoinPriceService;
            private readonly IMapper _mapper;

            public Handler(IBitcoinPriceService bitcoinPriceService, 
                IMapper mapper)
            {
                _bitcoinPriceService = bitcoinPriceService;
                _mapper = mapper;
            }
            
            public async Task<CurrencyVm> Handle(GetAllCurrenciesQuery request, 
                CancellationToken cancellationToken)
            {
                var currencies =
                    await GetCurrencies(cancellationToken);
                
                var orderedCurrencies = 
                    currencies
                        .OrderBy(x => x.Key);
                
                return _mapper.Map<CurrencyVm>(orderedCurrencies);
            }

            private async Task<Dictionary<string, CurrencyResponse>> GetCurrencies(CancellationToken cancellationToken)
            {
                var currencies =
                    await _bitcoinPriceService.GetCurrentPrices(cancellationToken);
                return currencies;
            }
        }
    }
}