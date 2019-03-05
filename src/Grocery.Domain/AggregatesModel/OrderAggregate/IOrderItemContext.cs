namespace Grocery.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderItemContext
    {
        int GetUnits();
        Price GetUnitPrice();
    }
}
