using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace DashboardApp.Controllers.Others
{
    public class MyAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            var assemblies = new List<Assembly>(baseAssemblies);
            Assembly controllersAssembly = Assembly.GetAssembly(typeof(MyAssembliesResolver));
            baseAssemblies.Add(controllersAssembly);
            return assemblies;
        }
    }
}
