using System.Collections.Generic;

namespace Centrum_zarządzania.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public string MainTitle => "Status czujników";

    public List<Sensor> SensorList { get; set; } = new List<Sensor> {
        new Sensor("Termometr", -32, 100),
        new Sensor("Miernik wilgotności", 0, 100),
        new Sensor("Czujnik odległości", 0, 400)
    };


}
