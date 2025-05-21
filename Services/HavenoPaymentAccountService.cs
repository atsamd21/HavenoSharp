using Grpc.Core;
using Haveno.Proto.Grpc;
using HavenoSharp.Extensions;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.PaymentAccounts;

namespace HavenoSharp.Services;

public interface IHavenoPaymentAccountService
{
    Task<Models.PaymentAccount> CreatePaymentAccountAsync(Models.Requests.CreatePaymentAccountRequest createPaymentAccountRequest, CancellationToken cancellationToken = default);
    Task<Models.PaymentAccountForm> GetPaymentAccountFormAsync(string paymentMethodId, CancellationToken cancellationToken = default);
    Task<List<Models.PaymentAccount>> GetPaymentAccountsAsync(CancellationToken cancellationToken = default);
    Task DeletePaymentAccountAsync(string paymentAccountId, CancellationToken cancellationToken = default);
    Task<Models.PaymentAccount> CreateCryptoCurrencyPaymentAccountAsync(Models.Requests.CreateCryptoCurrencyPaymentAccountRequest createCryptoCurrencyPaymentAccountRequest, CancellationToken cancellationToken = default);
    Task<List<Models.PaymentMethod>> GetCryptoCurrencyPaymentMethodsAsync(CancellationToken cancellationToken = default);
    Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync(CancellationToken cancellationToken = default);
    Task<string> ValidateFormFieldAsync(Models.Requests.ValidateFormFieldRequest validateFormFieldRequest, CancellationToken cancellationToken = default);
}

public sealed class HavenoPaymentAccountService : IHavenoPaymentAccountService
{
    private readonly GrpcChannelSingleton _grpcChannelService;
    private PaymentAccountsClient PaymentAccountsClient => new(_grpcChannelService.Channel);

    public HavenoPaymentAccountService(GrpcChannelSingleton grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
    }

    public async Task<Models.PaymentAccount> CreatePaymentAccountAsync(Models.Requests.CreatePaymentAccountRequest createPaymentAccountRequest, CancellationToken cancellationToken = default)
    {
        var adaptedRequest = createPaymentAccountRequest.Adapt<CreatePaymentAccountRequest>();
        adaptedRequest.PaymentAccountForm.Fields.AddRange(createPaymentAccountRequest.PaymentAccountForm.Fields.Select(x => x.Adapt<Protobuf.PaymentAccountFormField>()));

        for (int i = 0; i < createPaymentAccountRequest.PaymentAccountForm.Fields.Count; i++)
        {
            var sourceField = createPaymentAccountRequest.PaymentAccountForm.Fields[i];
            var destinationField = adaptedRequest.PaymentAccountForm.Fields[i];

            destinationField.SupportedSepaEuroCountries.AddRange(sourceField.SupportedSepaEuroCountries.Adapt<IEnumerable<Protobuf.Country>>());
            destinationField.SupportedCountries.AddRange(sourceField.SupportedCountries.Adapt<IEnumerable<Protobuf.Country>>());
            destinationField.SupportedSepaNonEuroCountries.AddRange(sourceField.SupportedSepaNonEuroCountries.Adapt<IEnumerable<Protobuf.Country>>());
            destinationField.RequiredForCountries.AddRange(sourceField.RequiredForCountries.Adapt<IEnumerable<string>>());

            destinationField.SupportedCurrencies.AddRange(sourceField.SupportedCurrencies.Adapt<IEnumerable<Protobuf.TradeCurrency>>());
        }

        var response = await PaymentAccountsClient.CreatePaymentAccountAsync(adaptedRequest, cancellationToken: cancellationToken);
        return response.PaymentAccount.Adapt<Models.PaymentAccount>();
    }

    public async Task<Models.PaymentAccountForm> GetPaymentAccountFormAsync(string paymentMethodId, CancellationToken cancellationToken = default)
    {
        var response = await PaymentAccountsClient.GetPaymentAccountFormAsync(new GetPaymentAccountFormRequest { PaymentMethodId = paymentMethodId }, cancellationToken: cancellationToken);
        return response.PaymentAccountForm.Adapt<Models.PaymentAccountForm>();
    }

    public async Task<List<Models.PaymentAccount>> GetPaymentAccountsAsync(CancellationToken cancellationToken = default)
    {
        var response = await PaymentAccountsClient.GetPaymentAccountsAsync(new GetPaymentAccountsRequest(), cancellationToken: cancellationToken);
        return response.PaymentAccounts.Adapt<List<Models.PaymentAccount>>();
    }

    public async Task DeletePaymentAccountAsync(string paymentAccountId, CancellationToken cancellationToken = default)
    {
        await PaymentAccountsClient.DeletePaymentAccountAsync(new DeletePaymentAccountRequest { PaymentAccountId = paymentAccountId }, cancellationToken: cancellationToken);
    }

    public async Task<Models.PaymentAccount> CreateCryptoCurrencyPaymentAccountAsync(Models.Requests.CreateCryptoCurrencyPaymentAccountRequest createCryptoCurrencyPaymentAccountRequest, CancellationToken cancellationToken = default)
    {
        var response = await PaymentAccountsClient.CreateCryptoCurrencyPaymentAccountAsync(createCryptoCurrencyPaymentAccountRequest.Adapt<CreateCryptoCurrencyPaymentAccountRequest>(), cancellationToken: cancellationToken);
        return response.PaymentAccount.Adapt<Models.PaymentAccount>();
    }

    public async Task<List<Models.PaymentMethod>> GetCryptoCurrencyPaymentMethodsAsync(CancellationToken cancellationToken = default)
    {
        var response = await PaymentAccountsClient.GetCryptoCurrencyPaymentMethodsAsync(new GetCryptoCurrencyPaymentMethodsRequest(), cancellationToken: cancellationToken);
        return response.PaymentMethods.Adapt<List<Models.PaymentMethod>>();
    }
    
    public async Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync(CancellationToken cancellationToken = default)
    {
        var response = await PaymentAccountsClient.GetPaymentMethodsAsync(new GetPaymentMethodsRequest(), cancellationToken: cancellationToken);
        return response.PaymentMethods.Adapt<List<Models.PaymentMethod>>();
    }

    public async Task<string> ValidateFormFieldAsync(Models.Requests.ValidateFormFieldRequest validateFormFieldRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            await PaymentAccountsClient.ValidateFormFieldAsync(validateFormFieldRequest.Adapt<ValidateFormFieldRequest>(), cancellationToken: cancellationToken);
            return string.Empty;
        }
        catch (RpcException e)
        {
            return e.GetErrorMessage();
        }
    }
}
