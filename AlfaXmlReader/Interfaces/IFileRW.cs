using AlfaXmlReader.Models;

namespace AlfaXmlReader.Interfaces;

public interface IFileRw
{
    string ReadFromFile();

    void WriteToExcel(ChannelModel data);
    void WriteToWord(ChannelModel data);
    void WriteToTxt(ChannelModel data);

    void WriteTo(string content, string filter);
}