﻿using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using WotDossier.Applications.View;

namespace WotDossier.Views
{
    /// <summary>
    /// Interaction logic for PlayerStatisticWindow.xaml
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPlayerServerStatisticView))]
    public partial class PlayerServerStatisticWindow : Window, IPlayerServerStatisticView
    {
        public PlayerServerStatisticWindow()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            KeyDown += Window_KeyDown;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
