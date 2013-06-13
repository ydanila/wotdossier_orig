﻿using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Research.DynamicDataDisplay;
using WotDossier.Applications.View;

namespace WotDossier
{
    /// <summary>
    /// Interaction logic for TankStatisticWindow.xaml
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(ITankStatisticView))]
    public partial class TankStatisticWindow : Window, ITankStatisticView
    {
        public ChartPlotter ChartRating
        {
            get { return Chart; }
        }

        public ChartPlotter ChartWinPercent
        {
            get { return Chart2; }
        }

        public ChartPlotter ChartAvgDamage
        {
            get { return Chart3; }
        }

        public TankStatisticWindow()
        {
            InitializeComponent();
        }
    }
}