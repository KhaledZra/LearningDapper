namespace DapperLDemo;

class Media : IsItem, IFormatedToString
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }
}