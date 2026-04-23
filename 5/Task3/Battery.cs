class Battery : CarPart, IElectricalComponent
{
    public Battery(string name) : base(name) { }
    public void CheckVoltage() => Console.WriteLine($"{Name}: Напряжение в норме (12.6V).");
}
