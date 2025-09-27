using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Centrum_zarządzania.Services;
using ReactiveUI;

namespace Centrum_zarządzania.ViewModels;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    public string MainTitle => "Status czujników";

    private Task UpdateTask;

    private SensorUpdater _updateHandler = new SensorUpdater();

    private ReadingLocalSender _readingHandler = new ReadingLocalSender();

    private bool _showMain = true;
    private bool _showDetails = false;

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

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
    [Required]
    public string DescriptionField { get => _descriptionField;
        set
        {
            this.RaiseAndSetIfChanged(ref _descriptionField, value);
            OnPropertyChanged(nameof(DescriptionField));
        } }

    private string _pathField = "";
    [Required]
    public string PathField { get => _pathField;
    set
        {
            this.RaiseAndSetIfChanged(ref _pathField, value);
            OnPropertyChanged(nameof(PathField));
        }
    }

    private double _minField = 0, _maxField = 0;
    [Required]
    public double MinField
    {
        get => _minField;
        set
        {
            this.RaiseAndSetIfChanged(ref _minField, value);
            OnPropertyChanged(nameof(MinField));
        }
    }

    public double MaxField
    {
        get => _maxField;
        set
        {
            this.RaiseAndSetIfChanged(ref _maxField, value);
            OnPropertyChanged(nameof(MaxField));
        }
    }

    private int _intervalField = 1000;
    public int IntervalField { get => _intervalField;
    set
        {
            this.RaiseAndSetIfChanged(ref _intervalField, value);
            OnPropertyChanged(nameof(IntervalField));
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

    public void SwitchToSensorEdit(LocalSensor SensorViewModel)
    {
        NewSensor = false;
        EditedSensor = SensorViewModel;
        LoadSensor();
        Debug.WriteLine("Loaded" + SensorViewModel.Path);
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
            _updateHandler.UnsubscribeSensor(_editedSensor);
            _readingHandler.UnsubscribeSensor(_editedSensor);
            OnPropertyChanged("LocalSensors");
            Debug.WriteLine("SensorViewModel deleted!");
            App.SaveConfig();
        }
        ResetSettingsPage();
        ShowMain = true;
    }

    private void SaveSettings()
    {
        if (NewSensor)
        {
            EditedSensor = new LocalSensor(DescriptionField, PathField, MinField, MaxField, IntervalField);
            LocalSensors.Add(EditedSensor);
            _updateHandler.SubscribeSensor(_editedSensor);
            _readingHandler.SubscribeSensor(_editedSensor);
            NewSensor = false;
            Debug.WriteLine("Saved new SensorViewModel!");
        }
        else
        {
            {
                EditedSensor.Description = DescriptionField;
                EditedSensor.Path = PathField;
                EditedSensor.MaxValue = MaxField;
                EditedSensor.MinValue = MinField;
                EditedSensor.Interval = IntervalField;
                Debug.WriteLine("SensorViewModel edit saved!");
            }
        }
        OnPropertyChanged("LocalSensors");
        App.SaveConfig();
    }

    private ObservableCollection<LocalSensor> _localSensors = new ObservableCollection<LocalSensor>
    {
    };

    public ObservableCollection<LocalSensor> LocalSensors { get => _localSensors; 
        set 
        { 
            foreach(var item in _localSensors)
            {
                _updateHandler.UnsubscribeSensor(item);
                _readingHandler.UnsubscribeSensor(item);
            }
            _localSensors = value;
            foreach(var item in _localSensors)
            {
                _updateHandler.SubscribeSensor(item);
                _readingHandler.SubscribeSensor(item);
            }
        }
    }

    public ObservableCollection<SensorViewModel> SensorList { get; set; } = new ObservableCollection<SensorViewModel>
    {

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

    private void LoadSensor()
    {
        DescriptionField = EditedSensor.Description; PathField = EditedSensor.Path;
        MaxField = EditedSensor.MaxValue; MinField = EditedSensor.MinValue;
        IntervalField = EditedSensor.Interval;
    }

    private void SaveAction()
    {
        SwitchToSensorList(true);
    }

    public MainViewModel()
    {
        IObservable<bool> formOk = this.WhenAnyValue(
            x => x.PathField, x => x.DescriptionField, x => x.MaxField, x => x.MinField, x => x.IntervalField, (PathField, DescriptionField, MaxField, MinField, IntervalField) =>
                !string.IsNullOrWhiteSpace(DescriptionField) && !string.IsNullOrWhiteSpace(PathField));
        SaveCommand = ReactiveCommand.Create(SaveAction, formOk);

        foreach (var SensorViewModel in LocalSensors)
        {
            SensorList.Add(SensorViewModel);
        }
    }

    private bool newSensor = false;

    public bool NewSensor { get => newSensor; private set { newSensor = value; OnPropertyChanged("NewSensor"); } }
}
