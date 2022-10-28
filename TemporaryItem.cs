namespace DapperLDemo;

class TemporaryItem : IsItem
{
    public int Id { get; set; }
    public int Label_Id { get; set; }
    public int Media_Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}