namespace CodeFamily.Core.Models;

public record FileDependency(
    string SourceFile,
    string TargetModule, // e.g., "System.IO" or "CodeFamily.Core.Services"
    string Type // "import", "inheritance", "call"
);