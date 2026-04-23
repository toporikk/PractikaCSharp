class Gearbox : CarPart, IMechanicalComponent
{
    public Gearbox(string name) : base(name) { }
    public void CheckWear() => Console.WriteLine($"{Name}: Износ шестерней в пределах нормы.");
}
