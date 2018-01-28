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
    private XDocument _doc;
    public XMLReader(string filename)
    {
        _doc = XDocument.Load(filename);
    }

    public List<string> GetLines()
    {
        List<XElement> lines = _doc.Element("Dialogue").Elements("Line").ToList();
        List<string> strings = new List<string>();
        foreach (XElement line in lines)
        {
            strings.Add(line.Value);
        }
        return strings;
    }

    public string GetHint()
    {
        List<XElement> hints = _doc.Element("Dialogue").Elements("Hint").ToList();
        List<string> strings = new List<string>();
        foreach (XElement hint in hints)
        {
            if (GameManager.instance.ghostsEncountered.Find(x => x.Equals(hint.Attribute("ghostName").Value)) == null) {
                return hint.Value;
            }
        }
        return null;
    }

}