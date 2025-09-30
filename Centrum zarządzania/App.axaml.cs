using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.ViewModels;
using Centrum_zarządzania.Views;
using Microsoft.EntityFrameworkCore;

namespace Centrum_zarządzania;

public partial class App : Application
{
    public static MainViewModel Main { get; set; }

    public static Configuration Config { get; set; }

    public static SensorContext db { get; private set; }

    public static Device? ThisDevice { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        DbConnectionData dbconfig = new DbConnectionData();
        if (LoadDbData(out dbconfig))
        {
            db = new SensorContext(dbconfig);
            db.Database.EnsureCreated();
        }

        else
        {
            dbconfig = new DbConnectionData();
            SaveDbData(dbconfig);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Main = new MainViewModel();
            ThisDevice = db.devices.Include(device => device.Sensors).Where(device => device.Name == dbconfig.device).First();
            if(ThisDevice != null)
            {
                foreach (var sensor in ThisDevice.Sensors)
                {
                    Main.AddSensor(new LocalSensor(sensor));
                }
            }
            else
            {
                ThisDevice = new Device(dbconfig.device);
                db.Add(ThisDevice);
                db.SaveChanges();
            }
                //LoadConfig();
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

    public static bool LoadDbData(out DbConnectionData data)
    {
        try
        {
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true
            };

            data = JsonSerializer.Deserialize<DbConnectionData>(File.ReadAllText("dbconfig.json"), options);
            return data != null;
        }
        catch (FileNotFoundException e)
        {
            data = new DbConnectionData();
            SaveDbData(data);
            return false;
        }
    }

    public static void SaveDbData(DbConnectionData data)
    {
        var options = new JsonSerializerOptions()
        {
            IncludeFields = true
        };

        string configJson = JsonSerializer.Serialize(data, options);
        File.WriteAllText("dbconfig.json", configJson);
    }

}
