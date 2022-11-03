namespace DapperLDemo;

class Order
{
    public int Customer_Id { get; set; }
    public DateOnly Purchase_Date { get; set; }
    public string? Return_Message { get; set; }
    public double Currently_Paid { get; set; }
}