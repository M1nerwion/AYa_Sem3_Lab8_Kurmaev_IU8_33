using ClassesLib;
using System;
using System.Dynamic;
using System.Xml.Serialization;

namespace task1_desirealize
{
    internal class Desirealize
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Animal));

            using (FileStream fs = new FileStream("C:\\Users\\gusar\\source\\repos\\AYa_Sem3_Lab8_Kurmaev_IU8_33\\task1_desirealize\\Cow.xml", FileMode.OpenOrCreate))
            {
                Animal? cow = xmlSerializer.Deserialize(fs) as Animal;
                Console.WriteLine(cow?.Country);
            }
        }
    }
}
