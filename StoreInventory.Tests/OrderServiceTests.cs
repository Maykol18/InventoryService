using StoreInventory.API.Models;
using StoreInventory.API.Services;

namespace StoreInventory.Tests;

public class OrderServiceTests
{
    IOrderService service = new OrderService();

    [Fact]
    public void Should_ReturnAListOfOrders()
    {
        var orders = service.GetAll();

        Assert.NotNull(orders);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_ReturnSpecificOrder_When_OrderFound(int id)
    {
        var order = service.GetById(id);

        Assert.NotNull(order);
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void Should_ReturnNull_When_OrderNotFound(int id)
    {
        var order = service.GetById(id);

        Assert.Null(order);
    }

    [Fact]
    public void Should_AddOrder_When_PlacingANewOrder()
    {
        // Given
        var order = new Order
        {
            Products = new List<OrderItem>
            {
                new OrderItem{ ProductId = 1, Quantity = 6},
                new OrderItem{ ProductId = 2, Quantity = 6},
                new OrderItem{ ProductId = 3, Quantity = 6}
            }
        };
        // When
        var countBeforeAdd = service.GetAll().Count;
        service.PlaceOrder(order);
        var countAfterAdd = service.GetAll().Count;
        // Then
        Assert.Equal(countBeforeAdd + 1, countAfterAdd);
    }

    [Theory]
    [InlineData(1, 1, 6)]
    [InlineData(2, 2, 6)]
    [InlineData(3, 3, 6)]
    public void Should_UpdateOrder_When_OrderFound(int id, int productId, int quantity)
    {
        var orderItem = new OrderItem { ProductId = productId, Quantity = quantity };
        var orderToUpdate = new Order { Products = new List<OrderItem> { orderItem } };

        service.UpdateOrder(id, orderToUpdate);

        Assert.Equal(orderItem.Quantity, service.GetById(id).Products.Find(o => o.ProductId == orderItem.ProductId)!.Quantity);
    }

    [Theory]
    [InlineData(3000, 1, 6)]
    [InlineData(9000, 2, 6)]
    public void Should_ThrowNullReferenceException_When_OrderNotFound(int id, int productId, int quantity)
    {
        Assert.Throws<NullReferenceException>(() =>
        {
            var orderItem = new OrderItem { ProductId = productId, Quantity = quantity };
            var orderToUpdate = new Order { Products = new List<OrderItem> { orderItem } };

            service.UpdateOrder(id, orderToUpdate);
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_CancelOrder_When_OrderFoundById(int id)
    {
        var countBeforeAdd = service.GetAll().Count;
        service.CancelOrder(id);
        var countAfterAdd = service.GetAll().Count;

        Assert.Equal(countBeforeAdd - 1, countAfterAdd);

        Assert.Null(service.GetById(id));
    }

    [Theory]
    [InlineData(3000)]
    [InlineData(9000)]
    public void Should_ThrowNullReferenceException_When_CancelingOrderNotFound(int id)
    {
        Assert.Throws<NullReferenceException>(() =>
        {
            var countBeforeAdd = service.GetAll().Count;
            service.CancelOrder(id);
            var countAfterAdd = service.GetAll().Count;
        });
    }
}