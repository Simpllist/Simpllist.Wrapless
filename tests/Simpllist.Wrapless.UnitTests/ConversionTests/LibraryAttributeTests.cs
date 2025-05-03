using System.Reflection;
using FluentAssertions;
using Simpllist.Wrapless;

namespace Simpllist.ConversionTests;

public class UshortConversionTests
{
    [Fact]
    public void True_Should_Return_1()
    {
        true.ToUshort().Should().Be(1);
    }
    
    [Fact]
    public void False_Should_Return_0()
    {
        false.ToUshort().Should().Be(0);
    }
    
    [Fact]
    [InlineData(1)]
    public void One_Should_Return_True(int value)
    {
        value.ToBoolean().Should().BeTrue();
    }
    
    [Fact]
    [InlineData(0)]
    public void Zero_Should_Return_False(int value)
    {
        value.ToBoolean().Should().BeFalse();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(7)]
    [InlineData(100)]
    public void GreaterThanOne_Should_Return_False(int value)
    {
        value.ToBoolean().Should().BeFalse();
    }
}
