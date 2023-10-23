using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net.Http;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.Timing;

namespace Dignite.Abp.Wechat;

public abstract class ApiService : IApiService
{
    public IServiceProvider ServiceProvider { get; set; }
    protected readonly object ServiceProviderLock = new object();
    protected TService LazyGetRequiredService<TService>(ref TService reference)
    {
        if (reference == null)
        {
            lock (ServiceProviderLock)
            {
                if (reference == null)
                {
                    reference = ServiceProvider.GetRequiredService<TService>();
                }
            }
        }

        return reference;
    }


    protected ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);
    private ILoggerFactory _loggerFactory;

    protected ICurrentTenant CurrentTenant => LazyGetRequiredService(ref _currentTenant);
    private ICurrentTenant _currentTenant;

    protected ILogger Logger => _lazyLogger.Value;
    private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

    protected ISettingProvider SettingProvider => LazyGetRequiredService(ref _settingProvider);
    private ISettingProvider _settingProvider;

    protected IHttpClientFactory ClientFactory => LazyGetRequiredService(ref _clientFactory);
    IHttpClientFactory _clientFactory;

    protected IClock Clock => LazyGetRequiredService(ref _clock);
    private IClock _clock;

    protected ApiService()
    {
    }
}
