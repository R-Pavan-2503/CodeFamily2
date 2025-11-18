using CodeFamily.Core.Models;

namespace CodeFamily.Core.Services;

public interface IParserService
{
    /// <summary>
    /// Analyzes a file's content to find dependencies using Tree-sitter.
    /// </summary>
    /// <param name="filePath">Used to determine the language (e.g., .cs, .js)</param>
    /// <param name="content">The raw code content</param>
    IEnumerable<FileDependency> ParseDependencies(string filePath, string content);
}