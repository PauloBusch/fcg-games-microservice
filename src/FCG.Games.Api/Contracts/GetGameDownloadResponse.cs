namespace FCG.Games.Api.Contracts;

public record GetGameDownloadResponse(
    DownloadModel Download
);

public record DownloadModel(
    Guid Key,
    string DownloadUrl,
    long FileSizeBytes,
    string Version,
    DateTime CreatedAt
);
