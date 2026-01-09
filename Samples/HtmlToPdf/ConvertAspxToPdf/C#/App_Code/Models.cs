using System;

[Serializable]
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[Serializable]
public class Purchase
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
