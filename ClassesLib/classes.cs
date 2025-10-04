using ClassesLib;
using System.Xml.Linq;


namespace ClassesLib
{
    public class CommentAttribute : Attribute
    {
        public string Comment { get; set; }
        public CommentAttribute() : this("Undefined") { }
        public CommentAttribute(string Comment) { this.Comment = Comment; }
    }

    public enum eClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    public enum eFavoriteFood
    {
        Meat,
        Plants,
        Everything
    }

    [Comment()]
    public abstract class Animal
    {
        protected eClassificationAnimal Classification;
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; } = false;
        public string Name { get; set; }
        public string WhatAnimal { get; set; }

        public Animal() : this("Undefined", false, "Undefined", "Undefined") { }

        public Animal(string Country, bool HideFromOtherAnimals, string Name, string WhatAnimal)
        {
            this.Country = Country;
            this.WhatAnimal = WhatAnimal;
            this.Name = Name;
            this.HideFromOtherAnimals = HideFromOtherAnimals;
        }

        public void Deconstruct(out string Country, out bool HideFromOtherAnimals, out string Name, out string WhatAnimal)
        {
            Country = this.Country;
            HideFromOtherAnimals = this.HideFromOtherAnimals;
            Name = this.Name;
            WhatAnimal = this.WhatAnimal;
        }

        public eClassificationAnimal GetClassificationAnimal(string animal)
        {
            switch (animal)
            {
                case "Cow":
                    return eClassificationAnimal.Herbivores;
                case "Lion":
                    return eClassificationAnimal.Carnivores;
                case "Pig":
                    return eClassificationAnimal.Omnivores;
            }
            return eClassificationAnimal.Omnivores;
        }

        public abstract eFavoriteFood GetFavoriteFood();

        public abstract void SayHello();
    }
}


[Comment("КОРОВА")]
public class Cow : Animal
{
    public Cow(string Country, bool HideFromOtherAnimals, string Name, string WhatAnimal) : base(Country, HideFromOtherAnimals, Name, WhatAnimal) { }//base() { }

    public override eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Plants;
    }

    public Cow() : base("Undefined", false, "Undefined", "Undefined") { }

    public override void SayHello()
    {
        Console.WriteLine("Moooo");
    }
}

[Comment("ЛЁВА")]
public class Lion : Animal
{
    public Lion(string Country, bool HideFromOtherAnimals, string Name, string WhatAnimal) : base(Country, HideFromOtherAnimals, Name, WhatAnimal) { }//base() { }

    public override eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Meat;
    }

    public override void SayHello()
    {
        Console.WriteLine("ROOOOAR");
    }
}

[Comment("ХРЮНЯ")]
public class Pig : Animal
{
    public Pig(string Country, bool HideFromOtherAnimals, string Name, string WhatAnimal) : base(Country, HideFromOtherAnimals, Name, WhatAnimal) { }//base() { }

    public override eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Everything;
    }

    public override void SayHello()
    {
        Console.WriteLine("ХРЮЮЮЮЮЮЮ");
    }
}