using System.Diagnostics;
using JuntoSeguros.Enterprise.Library.Contracts;

namespace JuntoSeguros.Enterprise.Library.Tracing;

public class EnterpriseActivityTag(Activity activity) : ITag
{
    public ITag SetTag(string key, object value)
    {
        activity.SetTag(key, value);
        return this;
    }
}