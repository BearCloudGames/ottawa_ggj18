using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class XMLReader
{
    public XMLReader()
    {

    }

    public List<string> ReadFile(string filename)
    {
        XDocument doc = XDocument.Load(filename);
        List<XElement> lines = doc.Element("Dialogue").Elements("Line").ToList();
        List<string> strings = new List<string>();
        foreach (XElement line in lines)
        {
            strings.Add(line.Value);
        }
        return strings;
    }

}