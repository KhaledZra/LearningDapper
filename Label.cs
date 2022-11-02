namespace DapperLDemo;

class Label : IsItem, IFormatedToString
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public override string ToString()
    {
        return $"{Name}";
    }
}