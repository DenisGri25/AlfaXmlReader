using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlfaXmlReader.Models;

[XmlRoot(ElementName = "item")]
public class ItemModel
{
    [XmlElement(ElementName = "title")]
    public Title Title { get; set; }
    [XmlElement(ElementName = "link")]
    public Link Link { get; set; }
    [XmlElement(ElementName = "description")]
    public Description Description { get; set; }
    [XmlElement(ElementName = "category")]
    public Category Category { get; set; }
    [XmlElement(ElementName = "pubDate")]
    public PubDate PubDate { get; set; }
}

[XmlRoot(ElementName="title")]  
public class Title {  
    [XmlText]  
    public string Text { get; set; }  
}  

[XmlRoot(ElementName="link")]  
public class Link {  
    [XmlText]  
    public string Text { get; set; }  
}  

[XmlRoot(ElementName="description")]  
public class Description {  
    [XmlText]  
    public string Text { get; set; }  
}  

[XmlRoot(ElementName="category")]  
public class Category {  
    [XmlText]  
    public string Text { get; set; }  
}  

[XmlRoot(ElementName="pubDate")]  
public class PubDate {  
    [XmlText]  
    public string Text { get; set; }  
}  

[XmlRoot(ElementName = "channel")]
public class ChannelModel
{
    [XmlElement(ElementName = "item")] public List<ItemModel>? Items { get; set; }
}