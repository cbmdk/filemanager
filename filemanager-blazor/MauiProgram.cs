using Microsoft.Extensions.Logging;
using filemanager_blazor.Data;
using filemanager_blazor.Helpers;
using filemanager_blazor.Utilities;

namespace filemanager_blazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        //builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IHttpService, HttpService>()
            .AddScoped<ILocalStorageService, LocalStorageService>();

        builder.Services.AddScoped(x => {
            var apiUrl = new Uri(builder.Configuration["apiUrl"]);

            // use fake backend if "fakeBackend" is "true" in appsettings.json
            if (builder.Configuration["fakeBackend"] == "true")
                return new HttpClient(new FakeBackendHandler()) { BaseAddress = apiUrl };

            return new HttpClient() { BaseAddress = apiUrl };
        });
        //var authenticationService = builder.Services.GetRequiredService<IAuthenticationService>();
        //await authenticationService.Initialize();

        return builder.Build();
	}
}

