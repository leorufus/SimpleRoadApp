// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Win32;
using RoadModel;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Lib.LandXML;



namespace SimpleDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {            
            this.InitializeComponent();
        }

        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenLandXMLFile();

            Corridor corridor = new Corridor(_data);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenLandXMLFile()
        {
            // Filter files by extension 
            var dlg = new OpenFileDialog
            {
                Filter = string.Join("|", "LandXML Files|*.xml")
            };
            dlg.FileOk += dlg_OpenLandXMLFile;
            dlg.ShowDialog(this);
        }

        Xml2CSharp.LandXML _data = new Xml2CSharp.LandXML();

        private void dlg_OpenLandXMLFile(object sender, CancelEventArgs e)
        {
            var dlg = sender as OpenFileDialog;
            if (dlg != null)
            {
                FileInfo file = new FileInfo(dlg.FileName);

                if (!file.Exists) // file does not exist; do nothing
                    return;

                try
                {
                    Loader load = new Loader();
                    _data = load.Load(file.FullName);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{file}-{exception.InnerException.Message}");
                }
            }
        }
    }
}