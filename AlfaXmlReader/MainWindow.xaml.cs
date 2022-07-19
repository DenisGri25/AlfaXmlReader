using System;
using System.Windows;
using AlfaXmlReader.Interfaces;
using AlfaXmlReader.Models;
using AlfaXmlReader.Services;

namespace AlfaXmlReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFileRw _fileRw;
        private readonly IXmlAlfaReader _xmlAlfaReader;
        private ChannelModel _readerData;

        public MainWindow()
        {
            _fileRw = new FileRw();
            _xmlAlfaReader = new XmlAlfaReader();
            InitializeComponent();
            ButtonExcel.IsEnabled = false;
            ButtonWord.IsEnabled = false;
            ButtonTxt.IsEnabled = false;
        }
        
        private void ButtonBase_OnClickSaveToExcel(object sender, RoutedEventArgs e)
        {
            if (_readerData.Items != null)
            {
                _fileRw.WriteToExcel(_readerData);
            }
        }

        private void ButtonBase_OnClickSaveToWord(object sender, RoutedEventArgs e)
        {
            if (_readerData.Items != null)
            {
                _fileRw.WriteToWord(_readerData);
            }
        }

        private void ButtonBase_OnClickSaveToTxt(object sender, RoutedEventArgs e)
        {
            if (_readerData.Items != null)
            {
                _fileRw.WriteToTxt(_readerData);
            }
            
        }

        private void ButtonBase_OnClickOpenFile(object sender, RoutedEventArgs e)
        {
            var path = _fileRw.ReadFromFile();
            (_readerData, var fileText) = _xmlAlfaReader.ReadFromFile(path);

            DisplayTextBlock.Text = fileText;
            if (_readerData.Items != null)
            {
                ButtonExcel.IsEnabled = true;
                ButtonWord.IsEnabled = true;
                ButtonTxt.IsEnabled = true;
            }
        }
    }
}