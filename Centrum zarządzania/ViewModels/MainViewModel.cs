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
    public LocalSensor EditedSensor { get =>  _editedSensor; private set { _editedSensor = value;  OnPropertyChanged("EditedSensor");  } }

    public void SwitchToNewSensor()
    {
        NewSensor = true;
        ResetSettingsPage();
        ShowMain = false;
    }

    public void SwitchToSensorEdit(LocalSensor sensor)
    {
        NewSensor = false;
        EditedSensor = sensor;
        LoadSensor();
        Debug.WriteLine("Loaded" + sensor.Path);
        ShowMain = false;
    }

    public void SwitchToSensorList(bool saveChanges)
    {
        if (saveChanges) SaveSettings();
        ResetSettingsPage();
        ShowMain = true;
    }

    public void DeleteCurrent()
    {
        if(!NewSensor)
        {
            LocalSensors.Remove(_editedSensor);
            OnPropertyChanged("LocalSensors");
            Debug.WriteLine("Sensor deleted!");
            App.SaveConfig();
        }
        ResetSettingsPage();
        ShowMain = true;
    }

    private ObservableCollection<LocalSensor> _localSensors = new ObservableCollection<LocalSensor>
    {
        //new LocalSensor("Temperatura", "temp.exe", -32, 100),
        //new LocalSensor("Wilgotność", "higro.exe", 0, 100),
        //new LocalSensor("Odległość", "dist.exe", 0, 400),
    };

    public ObservableCollection<LocalSensor> LocalSensors { get => _localSensors; 
        set 
        { 
            _localSensors = value; _updateHandler.sensorList = LocalSensors;
        }
    }

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

    private void ResetSettingsPage()
    {
        EditedSensor = new LocalSensor();
        LoadSensor();
    }

    private void SaveSettings()
    {
        if (NewSensor)
        {
            EditedSensor = new LocalSensor(DescriptionField, PathField, MinField, MaxField);
            LocalSensors.Add(EditedSensor);
            NewSensor = false;
            Debug.WriteLine("Saved new sensor!");
        }
        else
        {
            {
                EditedSensor.Description = DescriptionField;
                EditedSensor.Path = PathField;
                EditedSensor.MaxValue = MaxField;
                EditedSensor.MinValue = MinField;
                Debug.WriteLine("Sensor edit saved!");
            }
        }
        OnPropertyChanged("LocalSensors");
        App.SaveConfig();
    }

    private void LoadSensor()
    {
        DescriptionField = EditedSensor.Description; PathField = EditedSensor.Path;
        MaxField = EditedSensor.MaxValue; MinField = EditedSensor.MinValue;
    }

    public MainViewModel()
    {

        foreach (var sensor in LocalSensors)
        {
            SensorList.Add(sensor);
        }

        _updateHandler = new SensorUpdateService(LocalSensors);
    }

    private bool newSensor = false;

    public bool NewSensor { get => newSensor; private set { newSensor = value; OnPropertyChanged("NewSensor"); } }
}
