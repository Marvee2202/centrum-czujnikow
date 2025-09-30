using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Centrum_zarządzania.Models;
using Czytnik_Czujników.ViewModels;

namespace Czytnik_Czujników
{
    public partial class App : Application
    {
        public static SensorContext db { get; private set; }
        public static UpdateRunner ur { get; private set; }
        public static MainViewModel Main { get; set; }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            DbConnectionData dbconfig;
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

            Main = new MainViewModel();
            ur = new UpdateRunner();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = Main
                };
                //ur.Subscribe(Main);
                Main.GenerateList();
            }

            base.OnFrameworkInitializationCompleted();
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
}