﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioView.Views.Measurement
{
    /// <summary>
    /// Interaction logic for MeasurementView.xaml
    /// </summary>
    public partial class MeasurementsView : UserControl
    {
        public MeasurementsView()
        {
            InitializeComponent();
        }

        private void GraphSettingsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            String tag = ((MenuItem)sender).Tag.ToString();
            ((MeasurementsViewModel)DataContext).OnMinorGraphSettingsChanged(tag);
        }
    }
}
