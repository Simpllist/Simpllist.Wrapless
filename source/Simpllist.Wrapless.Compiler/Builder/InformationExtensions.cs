using System.Reflection;
using System.Text;

namespace Simpllist.Builder;

public static class InformationExtensions
{
    private const string SimplPlusModuleInformationType = "Simpllist.Wrapless.ISimplPlusModuleInformation";

    public static StringBuilder AppendInformationBuilder(this StringBuilder rootBuilder, Assembly assembly)
    {
        var inter = assembly.GetType(SimplPlusModuleInformationType);

        var moduleInformationType = assembly
            .GetTypes()
            .FirstOrDefault(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(inter));

        if (moduleInformationType is null)
        {
            return rootBuilder;
        }

        var info = Activator.CreateInstance(moduleInformationType);
        var build = moduleInformationType.GetProperty("Builder");

        if (build == null)
        {
            return rootBuilder;
        }

        var builder = build.GetValue(info) as StringBuilder;
        return rootBuilder.Append(builder);
    }
}