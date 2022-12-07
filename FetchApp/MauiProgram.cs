﻿using Microsoft.AspNetCore.Components.WebView.Maui;
using FetchApp.Data;

namespace FetchApp;

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
#endif
		
		builder.Services.AddSingleton<WeatherForecastService>();

		builder.Services.AddSingleton<IFirebaseService, FirebaseService>();
		builder.Services.AddSingleton<IDataHandler, DataHandler>();

		return builder.Build();
	}
}