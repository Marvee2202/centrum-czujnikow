using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.Services;

namespace Centrum_zarządzania.ViewModels;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    public string Greeting => "Welcome to Avalonia!";
    public string MainTitle => "Status czujników";

    private Task UpdateTask;

    private SensorUpdateService _updateHandler;

    private bool _showMain = true;
    private bool _showDetails = false;

    public bool ShowDetails
    {
        get { return _showDetails; }
        set
        {
            _showDetails = value;
            _showMain = !value;
            OnPropertyChanged(nameof(ShowDetails));
            OnPropertyChanged(nameof(ShowMain));
        }
    }

    public bool ShowMain
    {
        get => _showMain;
        set
        {
            _showMain = value;
            _showDetails = !value;
            OnPropertyChanged(nameof(ShowDetails));
            OnPropertyChanged(nameof(ShowMain));
        }
    }

    private string _descriptionField = "";
    public string DescriptionField { get => _descriptionField;
        set
        {
            _descriptionField = value;
            OnPropertyChanged(nameof(DescriptionField));
        } }

    private string _pathField = "";
    public string PathField { get => _pathField;
    set
        {
            _pathField = value;
            OnPropertyChanged(nameof(PathField));
        }
    }

    private double _minField = 0, _maxField = 0;
    public double MinField
    {
        get => _minField;
        set
        {
            _minField = value;
            OnPropertyChanged(nameof(MinField));
        }
    }
    public double MaxField
    {
        get => _maxField;
        set
        {
            _maxField = value;
            OnPropertyChanged(nameof(MaxField));
        }
    }


    private LocalSensor _editedSensor;
    public LocalSensor EditedSensor { get =>  _editedSensor; }

    public void SwitchToNewSensor()
    {
        ShowMain = false;
    }

    public void SwitchToSensorEdit(LocalSensor sensor)
    {
        ShowMain = false;
    }

    public void SwitchToSensorList(bool saveChanges)
    {
        ShowMain = true;
    }

    public void DeleteCurrent()
    {
        ShowMain = true;
    }

    public ObservableCollection<LocalSensor> LocalSensors { get; set; } = new ObservableCollection<LocalSensor> {
        new LocalSensor("Temperatura", "sense\\temp.exe", -32, 100),
        new LocalSensor("Wilgotność", "sense\\higro.exe", 0, 100),
        new LocalSensor("Odległość", "sense\\dist.exe", 0, 400),
    };

    public ObservableCollection<Sensor> SensorList { get; set; } = new ObservableCollection<Sensor>
    {
        //new Sensor("Termometr", -32, 100),
        //new Sensor("Miernik wilgotności", 0, 100),
        //new Sensor("Czujnik odległości", 0, 400)
    };

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainViewModel()
    {

        foreach (var sensor in LocalSensors)
        {
            SensorList.Add(sensor);
        }

        _updateHandler = new SensorUpdateService(LocalSensors);
    }
}
