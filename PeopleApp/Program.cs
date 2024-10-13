using PackLibrary;

Person john = new Person() { Name = "John" };
Person jane = new Person() { Name = "Jane" };
Person sarah = new Person() { Name = "Sarah" };

// Marry John and Jane
john.Marry(jane);

// Output John's spouses
john.OutputSpouses();

// Procreate with Jane
Person baby1 = john.ProcreateWith(jane);
baby1.Name = "John II";
Console.WriteLine($"{baby1.Name} was born on {baby1.Born}");

// Adopt child if no children
Person adoptedChild = new Person() { Name = "Alex", IsStepChild = true };
john.AdoptChild(adoptedChild);

// Show if the adopted child is a stepchild
if (john.CheckIfStepChild(adoptedChild))
{
    Console.WriteLine($"{adoptedChild.Name} is a stepchild.");
}

// Show parents of baby
john.ShowParents(baby1);

// Output John's children
john.WriteChildrenToConsole();
