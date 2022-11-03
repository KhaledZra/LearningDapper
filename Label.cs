namespace DapperLDemo;

class Label : Entity, IFormatedToString
{
    public override int Id { get; set; }
    public string? Name { get; set; }
    
    public override string ToString()
    {
        return $"{Name}";
    }

    public string FormatedToString()
    {
        return $"Name: {Name}";
    }
}