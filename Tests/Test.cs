using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests;

public class Test
{
    private readonly ITestOutputHelper _output;

    public Test(ITestOutputHelper output)
    {
        _output = output;
    }

}