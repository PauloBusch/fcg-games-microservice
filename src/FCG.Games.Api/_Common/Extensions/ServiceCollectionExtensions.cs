using FCG.Games.Api._Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FCG.Games.Api._Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFcgGamesApiSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "FCG Games API", Version = "v1" });
        });

        return services;
    }


    public static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        AuthenticationSettings authenticationSettings
    )
    {
        authenticationSettings.EnsureSettings();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.Authority = authenticationSettings.Authority;
            options.Audience = authenticationSettings.Audience;
            options.RequireHttpsMetadata = authenticationSettings.RequireHttpsMetadata;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = Claims.Role,
                NameClaimType = Claims.UserName,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(auth =>
        {
            auth.AddPolicy(Policies.OnlyAdmin, p => p
                .RequireClaim(Claims.Role, Roles.Admin));

            auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }

    private static void EnsureSettings(this AuthenticationSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Authority);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Audience);
    }
}
