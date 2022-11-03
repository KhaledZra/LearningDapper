namespace DapperLDemo;

class PermanentItem : Entity, IFormatedToString
{
    public int Label_Id { get; set; }
    public int Media_Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }

    public string ToString()
    {
        return $"Name: {Name} " +
               $"\nInfo: {Label_Id}, {Media_Id}" +
               $"\nPrice: {Price}";
    }
}