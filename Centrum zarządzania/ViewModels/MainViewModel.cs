using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.Services;

namespace Centrum_zarządzania.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public string MainTitle => "Status czujników";

    private Task UpdateTask;

    private SensorUpdateService _updateHandler;

    public List<LocalSensor> LocalSensors { get; set; } = new List<LocalSensor> {
        new LocalSensor("Temperatura", "sense\\temp.exe", -32, 100),
        new LocalSensor("Wilgotność", "sense\\higro.exe", 0, 100),
        new LocalSensor("Odległość", "sense\\dist.exe", 0, 400)
    };

    public List<Sensor> SensorList { get; set; } = new List<Sensor>
    {
        //new Sensor("Termometr", -32, 100),
        //new Sensor("Miernik wilgotności", 0, 100),
        //new Sensor("Czujnik odległości", 0, 400)
    };

    public MainViewModel()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        foreach (var sensor in LocalSensors)
        {
            SensorList.Add(sensor);
        }

        _updateHandler = new SensorUpdateService(LocalSensors);
    }
}
