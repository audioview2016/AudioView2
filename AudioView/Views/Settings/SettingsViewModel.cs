﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AudioView.Views.History;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using NLog;
using Prism.Commands;
using Prism.Mvvm;

namespace AudioView.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool isInitalizating = false;
        public List<string> Themes { get; set; }
        public List<string> Accents { get; set; }

        private static SettingsViewModel _instance;
        public static SettingsViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingsViewModel();
                }
                return _instance;
            }
        }

        private string _theme;
        public string Theme
        {
            get { return _theme; }
            set
            {
                SetProperty(ref _theme, value);
                UpdateSettings();
            }
        }

        private string _accent;
        public string Accent
        {
            get { return _accent; }
            set
            {
                SetProperty(ref _accent, value);
                UpdateSettings();
            }
        }
       
        private SettingsViewModel()
        {
            Accents = ThemeManager.Accents.Select(x => x.Name).ToList();
            Themes = ThemeManager.AppThemes.Select(x => x.Name).ToList();
            LoadSettings();
        }

        private void UpdateSettings()
        {
            if(isInitalizating)
                return;

            ThemeManager.ChangeAppStyle(Application.Current, 
                ThemeManager.Accents.Where(x=>x.Name ==  Accent).First(), 
                ThemeManager.AppThemes.Where(x => x.Name == Theme).First());
            SaveSettings();
        }

        private void SaveSettings()
        {
            var settingsString = JsonConvert.SerializeObject(new AudioViewSettings()
            {
                Accent = Accent,
                Theme = Theme
            });
            File.WriteAllText("settings.json", settingsString);
        }

        private void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                isInitalizating = true;
                Theme = "BaseDark"; // ThemeManager.Accents.Select(x=>x.Name).First(),
                Accent = "Amber"; // ThemeManager.AppThemes.Select(x => x.Name).First()
                isInitalizating = false;
                UpdateSettings();
                return;
            }

            var settings = JsonConvert.DeserializeObject<AudioViewSettings>(File.ReadAllText("settings.json"));

            isInitalizating = true;
            if (!Themes.Any(x => x == settings.Theme))
            {
                settings.Theme = "BaseDark";
                SaveSettings();
            }
            Theme = settings.Theme;

            if (!Accents.Any(x => x == settings.Accent))
            {
                settings.Accent = "Amber";
                SaveSettings();
            }
            Accent = settings.Accent;

            isInitalizating = false;

            ThemeManager.ChangeAppStyle(Application.Current,
                ThemeManager.Accents.Where(x => x.Name == Accent).First(),
                ThemeManager.AppThemes.Where(x => x.Name == Theme).First());
        }
        
        private ICommand _selectAllCommand;
        public ICommand SelectAllCommand
        {
            get
            {
                if (_selectAllCommand == null)
                {
                    _selectAllCommand = new DelegateCommand(() =>
                    {
                        setSettingsValues(true);
                    });
                }
                return _selectAllCommand;
            }
        }

        private ICommand _unselectAllCommand;
        public ICommand UnselectAllCommand
        {
            get
            {
                if (_unselectAllCommand == null)
                {
                    _unselectAllCommand = new DelegateCommand(() =>
                    {
                        setSettingsValues(false);
                    });
                }
                return _unselectAllCommand;
            }
        }

        private void setSettingsValues(bool value)
        {
            Type type = typeof(DataGridDisplayViewModel);
            foreach (var p in type.GetProperties())
            {
                if (p.GetValue(DataGridDisplayViewModel.Instance) is bool)
                {
                    p.SetValue(DataGridDisplayViewModel.Instance, value);
                }
            }
        }
    }

    public class AudioViewSettings
    {
        public string Accent { get; set; }
        public string Theme { get; set; }
    }
}