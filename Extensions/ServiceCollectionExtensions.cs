using HavenoSharp.Services;
using HavenoSharp.Singletons;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HavenoSharp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHavenoServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Default
            .EnableNonPublicMembers(true);

        TypeAdapterConfig<Models.TraditionalCurrency, Protobuf.TraditionalCurrency>.NewConfig()
            .MapWith(src => new Protobuf.TraditionalCurrency());

        services.TryAddSingleton<GrpcChannelSingleton>();
        services.TryAddSingleton<HavenoNotificationSingleton>();

        services.TryAddScoped<IHavenoWalletService, HavenoWalletService>();
        services.TryAddScoped<IHavenoAccountService, HavenoAccountService>();
        services.TryAddScoped<IHavenoTradeService, HavenoTradeService>();
        services.TryAddScoped<IHavenoOfferService, HavenoOfferService>();
        services.TryAddScoped<IHavenoPaymentAccountService, HavenoPaymentAccountService>();
        services.TryAddScoped<IHavenoVersionService, HavenoVersionService>();
        services.TryAddScoped<IHavenoDisputeService, HavenoDisputeService>();
        services.TryAddScoped<IHavenoPriceService, HavenoPriceService>();
        services.TryAddScoped<IHavenoXmrNodeService, HavenoXmrNodeService>();
        services.TryAddScoped<IHavenoTradeStatisticsService, HavenoTradeStatisticsService>();
        services.TryAddScoped<IHavenoServerService, HavenoServerService>();

        return services;
    }
}
