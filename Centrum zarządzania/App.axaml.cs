using System.IO;
using System.Text.Json;
using System.Threading;
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

    public static SensorContext db { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private static Semaphore _dbAccess = new Semaphore(0, 1);

    public static Semaphore DbAccess { get => _dbAccess; }

    public override void OnFrameworkInitializationCompleted()
    {
        db = new SensorContext();
        db.Database.EnsureCreated();
        _dbAccess.Release();

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

    public static void LoadConnectionData()
    {

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
