using Grpc.Core;
using Haveno.Proto.Grpc;
using HavenoSharp.Extensions;
using HavenoSharp.Singletons;
using Mapster;
using static Haveno.Proto.Grpc.PaymentAccounts;

namespace HavenoSharp.Services;

public interface IHavenoPaymentAccountService
{
    Task<Models.PaymentAccount> CreatePaymentAccountAsync(Models.Requests.CreatePaymentAccountRequest createPaymentAccountRequest);
    Task<Models.PaymentAccountForm> GetPaymentAccountFormAsync(string paymentMethodId);
    Task<List<Models.PaymentAccount>> GetPaymentAccountsAsync();
    Task DeletePaymentAccountAsync(string paymentAccountId);
    Task<Models.PaymentAccount> CreateCryptoCurrencyPaymentAccountAsync(Models.Requests.CreateCryptoCurrencyPaymentAccountRequest createCryptoCurrencyPaymentAccountRequest);
    Task<List<Models.PaymentMethod>> GetCryptoCurrencyPaymentMethodsAsync();
    Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync();
    Task<string> ValidateFormFieldAsync(Models.Requests.ValidateFormFieldRequest validateFormFieldRequest);
}

public sealed class HavenoPaymentAccountService : IHavenoPaymentAccountService, IDisposable
{
    private readonly PaymentAccountsClient _paymentAccountsClient;
    private readonly IGrpcChannelService _grpcChannelService;

    public HavenoPaymentAccountService(IGrpcChannelService grpcChannelService)
    {
        _grpcChannelService = grpcChannelService;
        _paymentAccountsClient = new(_grpcChannelService.Channel);
    }

    public async Task<Models.PaymentAccount> CreatePaymentAccountAsync(Models.Requests.CreatePaymentAccountRequest createPaymentAccountRequest)
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

        var response = await _paymentAccountsClient.CreatePaymentAccountAsync(adaptedRequest);
        return response.PaymentAccount.Adapt<Models.PaymentAccount>();
    }

    public async Task<Models.PaymentAccountForm> GetPaymentAccountFormAsync(string paymentMethodId)
    {
        var response = await _paymentAccountsClient.GetPaymentAccountFormAsync(new GetPaymentAccountFormRequest { PaymentMethodId = paymentMethodId });
        return response.PaymentAccountForm.Adapt<Models.PaymentAccountForm>();
    }

    public async Task<List<Models.PaymentAccount>> GetPaymentAccountsAsync()
    {
        var response = await _paymentAccountsClient.GetPaymentAccountsAsync(new GetPaymentAccountsRequest());
        return response.PaymentAccounts.Adapt<List<Models.PaymentAccount>>();
    }

    public async Task DeletePaymentAccountAsync(string paymentAccountId)
    {
        await _paymentAccountsClient.DeletePaymentAccountAsync(new DeletePaymentAccountRequest { PaymentAccountId = paymentAccountId });
    }

    public async Task<Models.PaymentAccount> CreateCryptoCurrencyPaymentAccountAsync(Models.Requests.CreateCryptoCurrencyPaymentAccountRequest createCryptoCurrencyPaymentAccountRequest)
    {
        var response = await _paymentAccountsClient.CreateCryptoCurrencyPaymentAccountAsync(createCryptoCurrencyPaymentAccountRequest.Adapt<CreateCryptoCurrencyPaymentAccountRequest>());
        return response.PaymentAccount.Adapt<Models.PaymentAccount>();
    }

    public async Task<List<Models.PaymentMethod>> GetCryptoCurrencyPaymentMethodsAsync()
    {
        var response = await _paymentAccountsClient.GetCryptoCurrencyPaymentMethodsAsync(new GetCryptoCurrencyPaymentMethodsRequest());
        return response.PaymentMethods.Adapt<List<Models.PaymentMethod>>();
    }
    
    public async Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync()
    {
        var response = await _paymentAccountsClient.GetPaymentMethodsAsync(new GetPaymentMethodsRequest());
        return response.PaymentMethods.Adapt<List<Models.PaymentMethod>>();
    }

    public async Task<string> ValidateFormFieldAsync(Models.Requests.ValidateFormFieldRequest validateFormFieldRequest)
    {
        try
        {
            await _paymentAccountsClient.ValidateFormFieldAsync(validateFormFieldRequest.Adapt<ValidateFormFieldRequest>());
            return string.Empty;
        }
        catch (RpcException e)
        {
            return e.GetErrorMessage();
        }
    }

    public void Dispose()
    {
        _grpcChannelService.Dispose();
    }
}
