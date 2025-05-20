using Haveno.Proto.Grpc;
using HavenoSharp.Models.Responses;
using Mapster;
using static Haveno.Proto.Grpc.Wallets;

namespace HavenoSharp.Services;

public interface IHavenoWalletService
{
    Task<Models.Balances> GetBalancesAsync();
    Task<string> RelayXmrTxAsync(string metadata);
    Task<string> GetXmrPrimaryAddressAsync();
    Task<CreateXmrTxResponse> CreateXmrTxAsync(Models.Requests.CreateXmrTxRequest createXmrTxRequest);
    Task<string> GetXmrSeedAsync();
    Task<List<Models.XmrTx>> GetXmrTxsAsync();
}

public sealed class HavenoWalletService : IHavenoWalletService, IDisposable
{
    private readonly WalletsClient _walletClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoWalletService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _walletClient = new(_grpcChannelService.Channel);
    }

    public async Task<Models.Balances> GetBalancesAsync()
    {
        var response = await _walletClient.GetBalancesAsync(new GetBalancesRequest());
        return new Models.Balances
        {
            AvailableXMRBalance = response.Balances.Xmr.AvailableBalance,
            PendingXMRBalance = response.Balances.Xmr.PendingBalance,
            XMRBalance = response.Balances.Xmr.Balance,
            ReservedOfferBalance = response.Balances.Xmr.ReservedOfferBalance,
            ReservedTradeBalance = response.Balances.Xmr.ReservedTradeBalance
        };
    }

    public async Task<string> RelayXmrTxAsync(string metadata)
    {
        var response = await _walletClient.relayXmrTxAsync(new RelayXmrTxRequest { Metadata = metadata });
        return response.Hash;
    }

    public async Task<string> GetXmrPrimaryAddressAsync()
    {
        var response = await _walletClient.GetXmrPrimaryAddressAsync(new GetXmrPrimaryAddressRequest());
        return response.PrimaryAddress;
    }

    public async Task<string> GetXmrSeedAsync()
    {
        var response = await _walletClient.GetXmrSeedAsync(new GetXmrSeedRequest());
        return response.Seed;
    }

    public async Task<List<Models.XmrTx>> GetXmrTxsAsync()
    {
        var response = await _walletClient.GetXmrTxsAsync(new GetXmrTxsRequest());
        return response.Txs.Adapt<List<Models.XmrTx>>();
    }

    public async Task<CreateXmrTxResponse> CreateXmrTxAsync(Models.Requests.CreateXmrTxRequest createXmrTxRequest)
    {
        var request = new CreateXmrTxRequest();
        request.Destinations.AddRange(createXmrTxRequest.Destinations.Select(x => new XmrDestination { Address = x.Address, Amount = x.Amount.ToString() }));

        var response = await _walletClient.CreateXmrTxAsync(request);

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

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
