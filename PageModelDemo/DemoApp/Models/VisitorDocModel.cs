using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DemoApp.Models
{
    public class VisitorDocModel : IVisitorModel, IDisposable
    {
        const string document = "appdoc.xml";
        
        static XmlSerializer serializer = new XmlSerializer(typeof(List<Visitor>), new XmlRootAttribute("Visitors"));

        public List<Visitor> Visitors {get; set;}

        public VisitorDocModel()
        {
            if(File.Exists(document))
            {
                using(var reader = new StreamReader(document))
                    Visitors = (List<Visitor>) serializer.Deserialize(reader);
            }
            else
                Visitors = new List<Visitor>();
        }

        public IEnumerable<Visitor> ReadVisitors() => Visitors;

        public void WriteVisitor(Visitor value)
        {
            Visitor visitor = Visitors.Find(entry => entry.Name == value.Name);
            if(visitor == null)
                Visitors.Add(value);
            else
                visitor.Revisit();
        }

        public void Dispose()
        {
            using(var writer = new StreamWriter(document))
                serializer.Serialize(writer, Visitors);
        }
    }
}

