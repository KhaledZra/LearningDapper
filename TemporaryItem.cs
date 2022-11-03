namespace DapperLDemo;

class TemporaryItem : Entity
{
    public override int Id { get; set; }
    public int Label_Id { get; set; }
    public int Media_Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}