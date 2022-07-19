using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using AlfaXmlReader.Interfaces;
using AlfaXmlReader.Models;

namespace AlfaXmlReader.Services;

public class XmlAlfaReader : IXmlAlfaReader
{
    public (ChannelModel,string) ReadFromFile(string path)
    {
        
        var channel = new ChannelModel();
        var fileText = string.Empty;

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ChannelModel));
            using var reader = new StringReader(File.ReadAllText(path));
            using var copyer = new StringReader(File.ReadAllText(path));
            channel = (ChannelModel)serializer.Deserialize(reader)!;
            fileText = copyer.ReadToEnd();
        }
        catch (Exception e)
        {
            MessageBox.Show("Неверный тип или структура файла!");
        }

        return (channel, fileText);
    }
}