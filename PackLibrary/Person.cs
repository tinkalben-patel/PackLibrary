using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackLibrary
{
    public class Person
    {
        #region Properties

        public string? Name { get; set; }
        public DateTimeOffset Born { get; set; }
        public List<Person> Children = new();

        // Allow multiple spouses to be stored for a person
        public List<Person> Spouses = new();

        // A readonly property to show if a person is married to anyone
        public bool Married => Spouses.Count > 0;

        // New property to mark if a child is a stepchild
        public bool IsStepChild { get; set; } = false;

        #endregion

        #region Methods

        public void WriteToConsole()
        {
            Console.WriteLine($"{Name} was born on a {Born:dddd}");
        }

        public void WriteChildrenToConsole()
        {
            string term = Children.Count == 1 ? "child" : "children";
            Console.WriteLine($"{Name} has {Children.Count} {term}.");
        }

        // Static method to marry two people
        public static void Marry(Person p1, Person p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);

            if (p1.Spouses.Contains(p2) || p2.Spouses.Contains(p1))
            {
                throw new ArgumentException($"{p1.Name} is already married to {p2.Name}.");
            }

            p1.Spouses.Add(p2);
            p2.Spouses.Add(p1);
        }

        // Instance method to marry
        public void Marry(Person partner)
        {
            Marry(this, partner);
        }

        public void OutputSpouses()
        {
            if (Married)
            {
                string term = Spouses.Count == 1 ? "person" : "people";
                Console.WriteLine($"{Name} is married to {Spouses.Count} {term}:");

                foreach (var spouse in Spouses)
                {
                    Console.WriteLine($"{spouse.Name}");
                }
            }
            else
            {
                Console.WriteLine($"{Name} is single.");
            }
        }

        // Static method to procreate
        public static Person Procreate(Person p1, Person p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);
            if (!p1.Spouses.Contains(p2) || !p2.Spouses.Contains(p1))
            {
                throw new ArgumentException($"{p1.Name} must be married to {p2.Name} to procreate with them.");
            }

            Person baby = new()
            {
                Name = $"Baby of {p1.Name} and {p2.Name}",
                Born = DateTimeOffset.Now
            };

            p1.Children.Add(baby);
            p2.Children.Add(baby);

            return baby;
        }

        // Instance method for procreation
        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }

        // Assignment Part 1: Check if a married couple has kids, and if not, allow them to adopt
        public void AdoptChild(Person child)
        {
            if (Children.Count == 0)
            {
                Children.Add(child);
                Console.WriteLine($"{Name} adopted {child.Name}.");
            }
            else
            {
                Console.WriteLine($"{Name} already has children and cannot adopt.");
            }
        }

        // Assignment Part 2: Implement a method to check if a child is a stepchild
        public bool CheckIfStepChild(Person child)
        {
            return child.IsStepChild;
        }

        // Assignment Part 3: Implement a method to show the parents of a baby
        public void ShowParents(Person child)
        {
            if (Children.Contains(child))
            {
                foreach (var spouse in Spouses)
                {
                    Console.WriteLine($"{child.Name}'s parents are {Name} and {spouse.Name}.");
                }
            }
            else
            {
                Console.WriteLine($"{child.Name} is not a child of {Name}.");
            }
        }

        #endregion

        #region Operators

        // Define the + operator to marry
        public static bool operator +(Person a, Person b)
        {
            Marry(a, b);
            return a.Married && b.Married;
        }

        // Define the * operator to procreate (multiply)
        public static Person operator *(Person a, Person b)
        {
            return Procreate(a, b);
        }

        #endregion
    }
}
