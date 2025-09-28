namespace FCG.Games.Domain;

public record DownloadDto(
    Guid Key,
    string DownloadUrl,
    long FileSizeBytes,
    string Version,
    DateTime CreatedAt
);
