using System.Diagnostics;
using JuntoSeguros.Enterprise.Library.Options;
using JuntoSeguros.Enterprise.Library.Contracts;

namespace JuntoSeguros.Enterprise.Library.Tracing;

public class EnterpriseActivityFactory(EnterpriceConfiguration config) : IActivity, IActivityFactory
{
    private Activity? _activity;
    private EnterpriseActivityTag? _tag;

    public IActivity Start(string identify)
    {
        var activitySource = new ActivitySource(config.Name, config.Version);
        _activity = activitySource.StartActivity(identify);

        if(_activity is not null)
            _tag = new EnterpriseActivityTag(_activity);

        return this;
    }

    public void Dispose()
    {
        _activity?.Dispose();
    }

    public ITag? Tag => _tag;
}