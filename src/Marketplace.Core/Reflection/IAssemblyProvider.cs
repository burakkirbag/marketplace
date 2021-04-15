using System.Collections.Generic;
using System.Reflection;

namespace Marketplace.Reflection
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();

        IList<string> AdditionalAssemblyNames { get; set; }
    }
}