using AlfaXmlReader.Models;

namespace AlfaXmlReader.Interfaces;

public interface IXmlAlfaReader
{
    (ChannelModel, string) ReadFromFile(string path);
}