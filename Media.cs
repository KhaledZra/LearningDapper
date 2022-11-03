namespace DapperLDemo;

class Media : Entity, IFormatedToString
{
    public override int Id { get; set; }
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }
}