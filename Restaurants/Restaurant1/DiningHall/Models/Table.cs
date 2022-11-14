using DiningHall.Models.Base;
using DiningHall.Models.Status;

namespace DiningHall.Models;

public class Table : BaseEntity
{
    public int OrderId { get; set; }
    public TableStatus TableStatus { get; set; }
}