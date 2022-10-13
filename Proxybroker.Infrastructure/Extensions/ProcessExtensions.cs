using System.Diagnostics;
using System.Text;

namespace Proxybroker.Infrastructure.Extensions;

public static class ProcessExtensions
{
    public static async Task<(string output, string error)> RunAsync(this Process process, string command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));

        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();

        process.StartInfo = new ProcessStartInfo("sh", $"-c \"{command}\"")
        {
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data != null) outputBuilder.AppendLine(args.Data);
        };
        process.ErrorDataReceived += (_, args) =>
        {
            if (args.Data != null) errorBuilder.AppendLine(args.Data);
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync(cancellationToken);

        return (outputBuilder.ToString(), errorBuilder.ToString());
    }

    public static async Task RunAsync(this Process process, string command,
        Action<string>? onOutputDataReceived,
        Action<string>? onErrorDataReceived,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));

        process.StartInfo = new ProcessStartInfo("sh", $"-c \"{command}\"")
        {
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data != null) onOutputDataReceived?.Invoke(args.Data);
        };
        process.ErrorDataReceived += (_, args) =>
        {
            if (args.Data != null) onErrorDataReceived?.Invoke(args.Data);
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync(cancellationToken);
    }
}