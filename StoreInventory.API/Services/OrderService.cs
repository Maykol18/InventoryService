using StoreInventory.API.Models;

namespace StoreInventory.API.Services;

public class OrderService : IOrderService
{
    public List<Product> MockProducts = new List<Product>
    {
        new Product{Id = 1, Name = "Colgate Luminus White", Price = 25.6m, Stock = 125},
        new Product{Id = 2, Name = "Colgate Triple Accion", Price = 35.8m, Stock = 130},
        new Product{Id = 3, Name = "Colgate Clean Mint", Price = 15.3m, Stock = 110}
    };
    List<Order> mockOrders = new List<Order>
    {
        new Order{ Id = 1,
            Products = new List<OrderItem>
            {
                new OrderItem{ ProductId = 1, Price = 25, Quantity = 3},
                new OrderItem{ ProductId = 2, Price = 25, Quantity = 3},
                new OrderItem{ ProductId = 3, Price = 25, Quantity = 3}
            },
            TotalAmount = 75,
            Date = DateTime.Now
        },
        new Order{ Id = 2,
            Products = new List<OrderItem>
            {
                new OrderItem{ ProductId = 1, Price = 25, Quantity = 3},
                new OrderItem{ ProductId = 2, Price = 25, Quantity = 3},
                new OrderItem{ ProductId = 3, Price = 25, Quantity = 3}
            },
            TotalAmount = 75,
            Date = DateTime.Now
        },
        new Order{ Id = 3,
            Products = new List<OrderItem>
            {
                new OrderItem{ ProductId = 1, Price = 25, Quantity = 3},
                new OrderItem{ ProductId = 2, Price = 25, Quantity = 3},
                new OrderItem{ ProductId = 3, Price = 25, Quantity = 3}
            },
            TotalAmount = 75,
            Date = DateTime.Now
        }
    };
    public void CancelOrder(int id)
    {
        var sale = mockOrders.FirstOrDefault(s => s.Id == id)!;

        ReturnProductsFromOrder(sale.Products);
        mockOrders.Remove(sale);
    }

    private void ReturnProductsFromOrder(List<OrderItem> products)
    {
        foreach (var item in products)
        {
            var product = MockProducts.FirstOrDefault(p => p.Id == item.ProductId)!;

            product.Stock += item.Quantity;
        }
    }

    public List<Order> GetAll()
    {
        var sales = mockOrders;

        return sales;
    }

    public Order GetById(int id)
    {
        var sale = mockOrders.FirstOrDefault(s => s.Id == id)!;

        return sale;
    }

    public void PlaceOrder(Order order)
    {
        var sale = new Order
        {
            Id = mockOrders.Count + 1,
            Date = DateTime.Now
        };
        AddProductToOrder(order.Products, sale);
        mockOrders.Add(sale);
    }

    private void AddProductToOrder(List<OrderItem> products, Order sale)
    {
        foreach (var item in products)
        {
            var product = MockProducts.FirstOrDefault(p => p.Id == item.ProductId)!;

            if(item.Quantity > product.Stock)
            {
                throw new Exception("Not enough stock");
            }

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                Price = product.Price
            };

            sale.Products.Add(orderItem);
            product.Stock -= item.Quantity;
            sale.TotalAmount += product.Price * item.Quantity;
        }
    }

    public Order UpdateOrder(int id, Order order)
    {
        var sale = mockOrders.FirstOrDefault(s => s.Id == id)!;

        UpdateProductFromOrder(order.Products, sale);

        return sale;
    }

    private void UpdateProductFromOrder(List<OrderItem> products, Order sale)
    {
        foreach (var item in products)
        {
            var product = MockProducts.FirstOrDefault(p => p.Id == item.ProductId)!;

            if (item.Quantity > product.Stock)
            {
                throw new Exception("Not enough Stock");
            }

            var orderItem = sale.Products.FirstOrDefault(oi => oi.ProductId == item.ProductId)!;

            if(orderItem.Quantity > item.Quantity)
            {
                product.Stock += orderItem.Quantity - item.Quantity;
                sale.TotalAmount -= (product.Price * orderItem.Quantity) - (product.Price * item.Quantity);
            }

            else if (orderItem.Quantity < item.Quantity)
            {
                product.Stock -= orderItem.Quantity - item.Quantity;
                sale.TotalAmount += (product.Price * item.Quantity) - (product.Price * orderItem.Quantity);
            }

            orderItem.Quantity = item.Quantity;
        }
    }
}