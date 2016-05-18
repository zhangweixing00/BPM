using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace WS.K2
{
    public sealed class SerializationHelper
    {
        private static readonly string CultureName = "zh-CHS";
        private SerializationHelper()
        {
        }

        public static T Deserialize<T>(string value)
        {
            using (var sr = new StringReader(value))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        public static T Deserialize<T>(string value, XmlAttributeOverrides overrides)
        {
            using (var sr = new StringReader(value))
            {
                var serializer = new XmlSerializer(typeof(T), overrides);
                return (T)serializer.Deserialize(sr);
            }
        }

        public static string Serialize(object model)
        {
            return Serialize(model, false, false);
        }

        public static string Serialize(object model, bool xmlHeader, bool xmlNameSpace)
        {
            using (var stream = new MemoryStream())
            {
                var returnValue = string.Empty;
                var doc = new XmlDocument();

                //去掉XML的名称空间 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add(String.Empty, String.Empty);

                var serializer = new XmlSerializer(model.GetType());
                if (xmlNameSpace)
                {
                    serializer.Serialize(stream, model);
                }
                else
                {
                    serializer.Serialize(stream, model, xmlns);
                }
                stream.Seek(0, SeekOrigin.Begin);
                doc.Load(stream);

                if (xmlHeader)
                {
                    returnValue = doc.OuterXml;
                }
                else
                {
                    //使用LastChild属性去掉<?xml version="1.0" encoding="utf-8"?>
                    returnValue = doc.LastChild.OuterXml;
                }

                return returnValue;
            }
        }

        public static string Serialize(object model, XmlAttributeOverrides overrides)
        {
            using (var stream = new MemoryStream())
            {
                var doc = new XmlDocument();
                var serializer = new XmlSerializer(model.GetType(), overrides);
                serializer.Serialize(stream, model);
                stream.Seek(0, SeekOrigin.Begin);
                doc.Load(stream);

                return doc.OuterXml;
            }
        }

        public static XmlAttributeOverrides GetEnumClassFilter(List<Type> typeList)
        {
            var overrides = new XmlAttributeOverrides();
            foreach (var type in typeList)
            {
                string[] names = System.Enum.GetNames(type);
                var values = System.Enum.GetValues(type);
                for (int i = 0; i < names.Length; i++)
                {
                    var attrs = new XmlAttributes();
                    var xmlEnum = new XmlEnumAttribute();
                    xmlEnum.Name = ((int)values.GetValue(i)).ToString(CultureName);
                    attrs.XmlEnum = xmlEnum;
                    overrides.Add(type, names[i], attrs);
                }
            }

            return overrides;
        }


        #region test XmlAttributeOverrides

        //public class Orchestra
        //{
        //    public Instrument[] Instruments;
        //}

        //public class Instrument
        //{
        //    public string Name;
        //}

        //public class Brass : Instrument
        //{
        //    public bool IsValved;
        //}

        //public class Run
        //{
        //    public void SerializeObject(string filename)
        //    {
        //        /* Each overridden field, property, or type requires 
        //        an XmlAttributes object. */
        //        XmlAttributes attrs = new XmlAttributes();

        //        /* Create an XmlElementAttribute to override the 
        //        field that returns Instrument objects. The overridden field
        //        returns Brass objects instead. */
        //        XmlElementAttribute attr = new XmlElementAttribute();
        //        attr.ElementName = "Brass";
        //        attr.Type = typeof(Brass);

        //        // Add the element to the collection of elements.
        //        attrs.XmlElements.Add(attr);

        //        // Create the XmlAttributeOverrides object.
        //        XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();

        //        /* Add the type of the class that contains the overridden 
        //        member and the XmlAttributes to override it with to the 
        //        XmlAttributeOverrides object. */
        //        attrOverrides.Add(typeof(Orchestra), "Instruments", attrs);

        //        // Create the XmlSerializer using the XmlAttributeOverrides.
        //        XmlSerializer s =
        //        new XmlSerializer(typeof(Orchestra), attrOverrides);

        //        // Writing the file requires a TextWriter.
        //        TextWriter writer = new StreamWriter(filename);

        //        // Create the object that will be serialized.
        //        Orchestra band = new Orchestra();

        //        // Create an object of the derived type.
        //        Brass i = new Brass();
        //        i.Name = "Trumpet";
        //        i.IsValved = true;
        //        Instrument[] myInstruments = { i };
        //        band.Instruments = myInstruments;

        //        // Serialize the object.
        //        s.Serialize(writer, band);
        //        writer.Close();
        //    }

        //    public void DeserializeObject(string filename)
        //    {
        //        XmlAttributeOverrides attrOverrides =
        //           new XmlAttributeOverrides();
        //        XmlAttributes attrs = new XmlAttributes();

        //        // Create an XmlElementAttribute to override the Instrument.
        //        XmlElementAttribute attr = new XmlElementAttribute();
        //        attr.ElementName = "Brass";
        //        attr.Type = typeof(Brass);

        //        // Add the XmlElementAttribute to the collection of objects.
        //        attrs.XmlElements.Add(attr);

        //        attrOverrides.Add(typeof(Orchestra), "Instruments", attrs);

        //        // Create the XmlSerializer using the XmlAttributeOverrides.
        //        XmlSerializer s =
        //        new XmlSerializer(typeof(Orchestra), attrOverrides);

        //        FileStream fs = new FileStream(filename, FileMode.Open);
        //        Orchestra band = (Orchestra)s.Deserialize(fs);
        //        Console.WriteLine("Brass:");

        //        /* The difference between deserializing the overridden 
        //        XML document and serializing it is this: To read the derived 
        //        object values, you must declare an object of the derived type 
        //        (Brass), and cast the Instrument instance to it. */
        //        Brass b;
        //        foreach (Instrument i in band.Instruments)
        //        {
        //            b = (Brass)i;
        //            Console.WriteLine(
        //            b.Name + "\n" +
        //            b.IsValved);
        //        }
        //    }
        //}

        #endregion
    }
}