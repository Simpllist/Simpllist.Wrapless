using System.Reflection;
using System.Text;

namespace Simpllist.Builder;

public static class InformationExtensions
{
    private const string SimplPlusModuleInformationType = "Simpllist.Wrapless.ILibraryInformation";

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
        var comments = moduleInformationType.GetProperty("CommentsBuilder");
        var includes = moduleInformationType.GetProperty("IncludeBuilder");

        if (comments == null || includes == null)
        {
            return rootBuilder;
        }

        var commentsBuilder = comments.GetValue(info) as StringBuilder;
        var includesBuilder = includes.GetValue(info) as StringBuilder;
        return rootBuilder.Append(commentsBuilder).Append(includesBuilder);
    }
}