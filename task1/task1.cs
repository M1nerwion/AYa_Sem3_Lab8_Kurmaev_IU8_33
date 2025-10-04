using ClassesLib;
using System;
using System.Dynamic;
using System.Xml.Serialization;

namespace task1
{
    internal class task1
    {
        static void Main(string[] args)
        {
            Cow cow = new Cow("Russia", false, "Буренка", "Cow");

            XmlSerializer xmlSerializer = new XmlSerializer(cow.GetType());

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("Cow.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, cow);

                Console.WriteLine("Object has been serialized");
            }

            XmlSerializer xmlSerializer1 = new XmlSerializer(typeof(Animal));

            using (FileStream fs = new FileStream("Cow.xml", FileMode.OpenOrCreate))
            {
                Animal? cow1 = xmlSerializer.Deserialize(fs) as Animal;
                Console.WriteLine(cow1?.Country);
            }
        }
    }
}
