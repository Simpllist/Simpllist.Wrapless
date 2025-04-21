using System.Reflection;
using FluentAssertions;
using Simpllist.Wrapless;

namespace Simpllist.GeneratorTests;

public class LibraryAttributeTests
{
    [Fact]
    public void Should_Generate_Interface()
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => 
                t is { IsClass: true, IsAbstract: false } && 
                t.IsAssignableTo(typeof(ILibraryInformation)))
            .Should()
            .NotBeNull("Any assembly with a Library Attribute should generate 1 ILibraryInformation instance");
    }
    
    [Fact]
    public void Should_Generate_Type()
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .First(t => 
                t is { IsClass: true, IsAbstract: false } && 
                t.IsAssignableTo(typeof(ILibraryInformation)))
            .Name
            .Should()
            .Be("Simpllist_Wrapless_UnitTestsModuleInformation", "The types names should be formatted based on the module name");
    }
    
    [Fact]
    public void LibraryName_ShouldBe_AssignedDefinedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.LibraryName.Should().Be("Simpllist.Wrapless.UnitTests");
    }
    
    [Fact]
    public void DealerName_ShouldBe_AssignedDefinedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.DealerName.Should().Be("Simpllist");
    }

    [Fact]
    public void SystemName_ShouldBe_AssignedDefinedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.SystemName.Should().Be("Wrapless");
    }

    [Fact]
    public void ProgrammerName_ShouldBe_AssignedDefinedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.Programmer.Should().Be("Eric Williams");
    }
    
    [Fact]
    public void Comments_ShouldBe_AssignedDefinedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.Comments.Should().Be("This module is for unit testing and development purposes");
    }

    [Fact]
    public void HelpFile_ShouldBe_AssignedDefinedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.PdfHelpFile.Should().Be("Simpllist.Wrapless.pdf");
    }
    
    [Fact]
    public void CommentsBuilder_Should_CreateExpectedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.CommentsBuilder.ToString().Should().Be("""
                                                    /*******************************************************************************************
                                                    SIMPL+ Module Information
                                                    *******************************************************************************************/
                                                    /*
                                                    Dealer Name: Simpllist 
                                                    System Name: Wrapless
                                                    Programmer: Eric Williams
                                                    Comments: This module is for unit testing and development purposes
                                                    */
                                                     
                                                    
                                                    """);
    }
    
    [Fact]
    public void IncludeBuilder_Should_CreateExpectedValue()
    {
        var info = new Simpllist_Wrapless_UnitTestsModuleInformation();

        info.IncludeBuilder
            .ToString()
            .Contains("#USER_SIMPLSHARP_LIBRARY \"Simpllist.Wrapless.UnitTests\"")
            .Should()
            .BeTrue();
    }
    
}
