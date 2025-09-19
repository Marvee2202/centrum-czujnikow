using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.ViewModels;
using Centrum_zarządzania.Views;

namespace Centrum_zarządzania;

public partial class App : Application
{
    public static MainViewModel Main { get; set; }

    public static Configuration Config { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Main = new MainViewModel();
            LoadConfig();
            desktop.MainWindow = new MainWindow
            {
                DataContext = Main
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static void LoadConfig()
    {
        try
        {
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true
            };

            Config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText("config.json"), options);
        }
        catch (FileNotFoundException e)
        {
            Config = new Configuration();
        }
        if (Config != null)
        {
            Main.LocalSensors = Config.LocalSensorList;
        }
    }

    public static void SaveConfig()
    {
        Config.LocalSensorList = Main.LocalSensors;

        var options = new JsonSerializerOptions()
        {
            IncludeFields = true
        };

        string configJson = JsonSerializer.Serialize(Config, options);
        File.WriteAllText("config.json", configJson);
    }

}
