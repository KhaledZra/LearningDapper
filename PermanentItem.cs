namespace DapperLDemo;

class PermanentItem : Entity, IFormatedToString
{
    public override int Id { get; set; }
    public int Label_Id { get; set; }
    public int Media_Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }

    public string ToString()
    {
        return $"{Name}, " +
               $"{Label_Id}, {Media_Id}, " +
               $"{Price}";
    }
    
    public string FormatedToString()
    {
        return $"Name: {Name} " +
               $"\nInfo: {Label_Id}, {Media_Id}" +
               $"\nPrice: {Price}";
    }
}