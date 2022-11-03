namespace DapperLDemo;

class Label : Entity, IFormatedToString
{
    public string? Name { get; set; }
    
    public override string ToString()
    {
        return $"{Name}";
    }
}