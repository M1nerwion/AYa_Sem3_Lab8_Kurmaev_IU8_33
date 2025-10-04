using ClassesLib;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace task1_desirealize
{
    internal class Desirealize
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Cow));

            using (FileStream fs = new FileStream("Cow.xml", FileMode.OpenOrCreate))
            {
                Animal? cow = xmlSerializer.Deserialize(fs) as Animal;
                Console.WriteLine($"Name: {cow?.Name}, Country: {cow?.Country}, WhatAnimal: {cow?.WhatAnimal}, HideFromOther: {cow?.HideFromOtherAnimals}");
            }
        }
    }
}
