using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Serialization;
using AlfaXmlReader.Interfaces;
using AlfaXmlReader.Models;
using Aspose.Words;
using Aspose.Words.Tables;
using Microsoft.Win32;
using OfficeOpenXml;
using Font = Aspose.Words.Font;
using Underline = Aspose.Words.Underline;

namespace AlfaXmlReader.Services;

public class FileRw : IFileRw
{
    public string ReadFromFile()
    {
        var result = string.Empty;
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = false,
            Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*"
        };
        if(openFileDialog.ShowDialog() == true)
        {
           result = openFileDialog.FileName;
        }

        return result;
    }

    public void WriteTo(string content, string filter)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = filter
        };
        if(saveFileDialog.ShowDialog() == true)
            File.WriteAllText(saveFileDialog.FileName, content);
    }
    
    public void WriteToExcel(ChannelModel data)
    {
        var filters = "Excel file (*.xlsx)|*.xlsx";
        var saveFileDialog = new SaveFileDialog
        {
            Filter = filters
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(saveFileDialog.FileName);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Link", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Category", typeof(string));
            dataTable.Columns.Add("PubDate", typeof(string));

            for (int i = 0; i < data.Items.Count; i++)
            {
                var temp = data.Items.ElementAt(i);
                dataTable.Rows.Add(temp.Title.Text, temp.Link.Text, temp.Description.Text, temp.Category.Text, temp.PubDate.Text);
            }
            
            var ws = package.Workbook.Worksheets.Add("Channel");

            var range = ws.Cells["A1"].LoadFromDataTable(dataTable, true);
            range.AutoFitColumns();

            package.Save();
        }

    }
    
    public void WriteToWord(ChannelModel data)
    {
        var filters = "Word file (*.docx)|*.docx";
        // string tempPath = @"Src/temp.docx";
        
        var saveFileDialog = new SaveFileDialog
        {
            Filter = filters
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            // Set Up doc
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            // font config
            Font font = builder.Font;
            font.Size = 8;
            font.Color = Color.Black;
            font.Name = "Arial";
            font.Underline = Underline.None;

            // Set Up Data table
            var table = builder.StartTable();

            builder.InsertCell();
            table.AutoFit(AutoFitBehavior.AutoFitToContents);
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.Write("Title");
            builder.InsertCell();
            builder.Write("Link");
            builder.InsertCell();
            builder.Write("Description");
            builder.InsertCell();
            builder.Write("Category");
            builder.InsertCell();
            builder.Write("PubDate");
            builder.EndRow();

            foreach (var item in data.Items)
            {
                builder.InsertCell();
                builder.Write(item.Title.Text);
                builder.InsertCell();
                builder.Write(item.Link.Text);
                builder.InsertCell();
                builder.Write(item.Description.Text);
                builder.InsertCell();
                builder.Write(item.Category.Text);
                builder.InsertCell();
                builder.Write(item.PubDate.Text);
                builder.EndRow();
            }

            builder.EndTable();

            doc.Save(saveFileDialog.FileName);
        }
    }
    
    public void WriteToTxt(ChannelModel data)
    {
        var filters = "Text file (*.txt)|*.txt";
        var xmlSerializer = new XmlSerializer(typeof(ChannelModel));
        using var strWrt = new StringWriter();
        using var xmlWrt = XmlWriter.Create(strWrt);
        xmlSerializer.Serialize(xmlWrt, data);
        
        WriteTo(strWrt.ToString(), filters);
    }
    
}