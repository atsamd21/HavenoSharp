using Haveno.Proto.Grpc;
using HavenoSharp.Models.Responses;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.Wallets;

namespace HavenoSharp.Services;

public interface IHavenoWalletService
{
    Task<Models.Balances> GetBalancesAsync(CancellationToken cancellationToken = default);
    Task<string> RelayXmrTxAsync(string metadata, CancellationToken cancellationToken = default);
    Task<string> GetXmrPrimaryAddressAsync(CancellationToken cancellationToken = default);
    Task<CreateXmrTxResponse> CreateXmrTxAsync(Models.Requests.CreateXmrTxRequest createXmrTxRequest, CancellationToken cancellationToken = default);
    Task<string> GetXmrSeedAsync(CancellationToken cancellationToken = default);
    Task<List<Models.XmrTx>> GetXmrTxsAsync(CancellationToken cancellationToken = default);
}

public sealed class HavenoWalletService : IHavenoWalletService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private WalletsClient WalletClient => new(_grpcChannelService.Channel);

    public HavenoWalletService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<Models.Balances> GetBalancesAsync(CancellationToken cancellationToken = default)
    {
        var response = await WalletClient.GetBalancesAsync(new GetBalancesRequest(), cancellationToken: cancellationToken);
        return new Models.Balances
        {
            AvailableXMRBalance = response.Balances.Xmr.AvailableBalance,
            PendingXMRBalance = response.Balances.Xmr.PendingBalance,
            XMRBalance = response.Balances.Xmr.Balance,
            ReservedOfferBalance = response.Balances.Xmr.ReservedOfferBalance,
            ReservedTradeBalance = response.Balances.Xmr.ReservedTradeBalance
        };
    }

    public async Task<string> RelayXmrTxAsync(string metadata, CancellationToken cancellationToken = default)
    {
        var response = await WalletClient.relayXmrTxAsync(new RelayXmrTxRequest { Metadata = metadata }, cancellationToken: cancellationToken);
        return response.Hash;
    }

    public async Task<string> GetXmrPrimaryAddressAsync(CancellationToken cancellationToken = default)
    {
        var response = await WalletClient.GetXmrPrimaryAddressAsync(new GetXmrPrimaryAddressRequest(), cancellationToken: cancellationToken);
        return response.PrimaryAddress;
    }

    public async Task<string> GetXmrSeedAsync(CancellationToken cancellationToken = default)
    {
        var response = await WalletClient.GetXmrSeedAsync(new GetXmrSeedRequest(), cancellationToken: cancellationToken);
        return response.Seed;
    }

    public async Task<List<Models.XmrTx>> GetXmrTxsAsync(CancellationToken cancellationToken = default)
    {
        var response = await WalletClient.GetXmrTxsAsync(new GetXmrTxsRequest(), cancellationToken: cancellationToken);
        return response.Txs.Adapt<List<Models.XmrTx>>();
    }

    public async Task<CreateXmrTxResponse> CreateXmrTxAsync(Models.Requests.CreateXmrTxRequest createXmrTxRequest, CancellationToken cancellationToken = default)
    {
        var request = new CreateXmrTxRequest();
        request.Destinations.AddRange(createXmrTxRequest.Destinations.Select(x => new XmrDestination { Address = x.Address, Amount = x.Amount.ToString() }));

        var response = await WalletClient.CreateXmrTxAsync(request, cancellationToken: cancellationToken);

        return new CreateXmrTxResponse 
        { 
            Fee = response.Tx.Fee,
            Hash = response.Tx.Hash,
            Height = response.Tx.Height,
            IsConfirmed = response.Tx.IsConfirmed,
            IsLocked = response.Tx.IsLocked,
            Metadata = response.Tx.Metadata,
            Timestamp = response.Tx.Timestamp,
            IncomingTransfers = response.Tx.IncomingTransfers.Select(x => new Models.XmrIncomingTransfer
            {
                AccountIndex = x.AccountIndex,
                Address = x.Address,
                Amount = x.Amount,
                NumSuggestedConfirmations = x.NumSuggestedConfirmations,
                SubaddressIndex = x.SubaddressIndex
            }).ToList(),
            OutgoingTransfer = new Models.XmrOutgoingTransfer 
            {
                AccountIndex = response.Tx.OutgoingTransfer.AccountIndex,
                Amount = response.Tx.OutgoingTransfer.Amount,
                SubaddressIndices = response.Tx.OutgoingTransfer.SubaddressIndices.ToList(),
                Destinations = response.Tx.OutgoingTransfer.Destinations.Select(x => new Models.XmrDestination 
                { 
                    Address = x.Address, 
                    Amount = ulong.Parse(x.Amount) 
                }).ToList()
            }
        };
    }
}
