using System.Diagnostics;
using System.Text;
using ProxybrokerWeb.Infrastructure.Extensions;
using Shouldly;

namespace ProxybrokerWeb.Tests.Infrastructure.Extensions;

public class Tests
{
    [Test]
    public async Task Should_Run_Process_By_Whole()
    {
        var process = new Process();

        var (output, error) = await process.RunAsync("ls -a");

        output.ShouldNotBeEmpty();
    }

    [Test]
    public async Task Should_Read_Process_By_Event()
    {
        var process = new Process();

        var output = new StringBuilder();
        var error = new StringBuilder();

        void OnOutputReceived(string data) => output.AppendLine(data);

        void OnErrorReceived(string data) => error.AppendLine(data);

        await process.RunAsync(
            "ls -a",
            OnOutputReceived,
            OnErrorReceived);

        output.ToString().ShouldNotBeEmpty();
    }
}