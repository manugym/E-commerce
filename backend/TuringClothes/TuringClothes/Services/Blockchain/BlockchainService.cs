using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Stripe;
using System.Numerics;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Repository;

namespace TuringClothes.Services.Blockchain
{
    public class BlockchainService
    {
        private readonly TemporaryOrderRepository _temporaryOrderRepository;

        public BlockchainService(TemporaryOrderRepository temporaryOrderRepository)
        {
            _temporaryOrderRepository = temporaryOrderRepository;
        }

        public async Task<Erc20ContractDto> GetContractInfoAsync(string nodeUrl, string contractAddress)
        {
            Web3 web3 = new Web3(nodeUrl);
            Contract contract = web3.Eth.GetContract(ERC20ABI, contractAddress);

            string name = await contract.GetFunction("name").CallAsync<string>();
            string symbol = await contract.GetFunction("symbol").CallAsync<string>();
            int decimals = await contract.GetFunction("decimals").CallAsync<int>();
            BigInteger totalSupply = await contract.GetFunction("totalSupply").CallAsync<BigInteger>();

            return new Erc20ContractDto
            {
                Name = name,
                Symbol = symbol,
                Decimals = decimals,
                TotalSupply = totalSupply.ToString()
            };
        }
        public async Task<PurchaseInfoDto> GetEthereumPrice(long id)
        {
            var order = await _temporaryOrderRepository.GetTemporaryOrder(id);
            decimal price = 0;

            foreach (var item in order.Details)
            {
                price += item.Product.Price * item.Amount;
            }
            price = price / 100;
            CoinGeckoApi coinGeckoApi = new CoinGeckoApi();
            decimal ethEurPrice = await coinGeckoApi.GetEthereumPriceAsync();
            order.EthereumPrice = (double)ethEurPrice;
            await _temporaryOrderRepository.UpdateAsync(order);
            return new PurchaseInfoDto
            {
                TemporaryOrder = order,
                TotalPrice = price,
                EthereumPrice = ethEurPrice.ToString(),
                PriceInWei = (price / ethEurPrice).ToString()
            };

        }

        public async Task<EthereumTransaction> CreateEthTransaction(CreateTransactionRequest data)
        {
            var temporaryOrder = await _temporaryOrderRepository.GetTemporaryOrder(data.TemporaryOrderId);
            EthereumService ethereumService = new EthereumService(_temporaryOrderRepository);

            BigInteger value = ethereumService.ToWei((double)((temporaryOrder.TotalPriceEur / 100) / temporaryOrder.EthereumPrice));
            HexBigInteger gas = ethereumService.GetGas();
            HexBigInteger gasPrice = await ethereumService.GetGasPriceAsync();

            //temporaryOrder.EthereumPrice = value.ToString();
            //await _temporaryOrderRepository.UpdateAsync(temporaryOrder);

            temporaryOrder.HexEthereumPrice = new HexBigInteger(value).HexValue;
            await _temporaryOrderRepository.UpdateAsync(temporaryOrder);
            return new EthereumTransaction
            {
                Value = new HexBigInteger(value).HexValue,
                Gas = gas.HexValue,
                GasPrice = gasPrice.HexValue,
            };
        }

        public Task<bool> CheckTransactionAsync(CheckTransactionRequest data)
        {
            EthereumService ethereumService = new EthereumService(_temporaryOrderRepository);

            return ethereumService.CheckTransactionAsync(data.Hash, data.TemporaryOrderId);
        }


        // Definición del ABI de ERC-20
        private static readonly string ERC20ABI = """
    [
        {
            'constant': true,
            'inputs': [],
            'name': 'name',
            'outputs': [
                {
                    'name': '',
                    'type': 'string'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'constant': true,
            'inputs': [],
            'name': 'symbol',
            'outputs': [
                {
                    'name': '',
                    'type': 'string'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'constant': true,
            'inputs': [],
            'name': 'decimals',
            'outputs': [
                {
                    'name': '',
                    'type': 'uint8'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'constant': true,
            'inputs': [],
            'name': 'totalSupply',
            'outputs': [
                {
                    'name': '',
                    'type': 'uint256'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        }
    ]
    """;
    }
}
