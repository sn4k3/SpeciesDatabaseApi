using System.Collections;
using System.CommandLine;
using SpeciesDatabaseApi;

namespace SpeciesDatabaseCmd;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Query specific taxonomy and species database")
        {
            WormsCommand.CreateCommand(),
            IucnCommand.CreateCommand(),
            MarineRegionsCommand.CreateCommand(),
            SpeciesPlusCommand.CreateCommand()
        };
        
        try
        {
            await rootCommand.InvokeAsync(args);
        }
        catch (Exception e)
        {
            WriteLineError(e.Message);
        }
        
        return 1;
    }

    internal static string GetRootCommandDescription(BaseClient client) => $"Query - {client.ClientFullName} ({client.WebsiteUrl})";

    internal static string FormatResults(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        return text.Replace(", ", "\n");
    }

    internal static void Print(object? result)
    {
        if (result is null)
        {
            WriteLineWarning("No data returned.");
            return;
        }

        if (result is string str)
        {
            WriteLine(FormatResults(str));
            return;
        }

        if (result is IEnumerable enumerator)
        {
            int i = 0;
            foreach (var obj in enumerator)
            {
                if (obj is null) continue;
                i++;
                WriteLine($"################# {i} #################");
                WriteLine(FormatResults(obj.ToString()));
            }

            if (i == 0) WriteLineWarning("No results.");
            return;
        }

        WriteLine(FormatResults(result.ToString()));
    }

    #region Write to console methods
    internal static void Write(object? obj)
    {
        Console.Write(obj);
    }

    internal static void WriteLine(object? obj)
    {
        Console.WriteLine(obj);
    }

    internal static void WriteWarning(object? obj)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(obj);
        Console.ResetColor();
    }

    internal static void WriteLineWarning(object? obj)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(obj);
        Console.ResetColor();
    }

    internal static void WriteError(object? obj, bool exit = false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(obj);
        Console.ResetColor();
        if (exit) Environment.Exit(-1);
    }

    internal static void WriteLineError(object? obj, bool exit = true, bool prependError = true)
    {
        var str = obj?.ToString();
        if (str is not null && prependError && !str.StartsWith("Error:")) str = $"Error: {str}";

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ResetColor();
        if (exit) Environment.Exit(-1);
    }
    #endregion
}