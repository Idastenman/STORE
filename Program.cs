using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; } 
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; }

    public Product(string name, decimal price, int stockQuantity, string description)
    {
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        Description = description;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Product Name: {Name}");
        Console.WriteLine($"Price:{Price}");
        Console.WriteLine($"In Stock:{StockQuantity}");
        Console.WriteLine($"Description: {Description}");
    }
}

class Shoppingcart
{
    private List<Product> items = new List<Product>();

    public void AddToCart(Product product)
    {
        items.Add(product);
    }
    public void ListCartContent()
    {
        Console.WriteLine("Your Shopping Cart");
        foreach (var product in items)
        {
            Console.WriteLine($"{product.Name} - ${product.Price}");
        }
    }
    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in items)
        {
            totalCost += product.Price;
        }
        return totalCost;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Product product1 = new("Snus Loop", 42, 10, "Chili Melon, 4/5, 9,4mg/portion");
        Product product2 = new("Snus Zyn", 40, 5, "Ice Cool, 3/5, 6,9mg/portion");
        Product product3 = new("Snus Velo", 46, 7, "Freeze, 5/5, 11,2mg/portion");
        Product product4 = new("Redbull small", 19, 12, "250ml");
        Product product5 = new("Redbull medium", 19, 12, "473ml");

        List<Product> availableproducts = new List<Product> { product1, product2, product3, product4, product5 };

        decimal userCurrency = 100;
        Shoppingcart cart = new Shoppingcart();

        Console.WriteLine("Welcome to the store! Available Products:");
        Console.WriteLine("-----------------------------------------");
        foreach (var product in availableproducts)
        {
            Console.WriteLine($"{product.Name} - {product.Price} (In Stock: {product.StockQuantity})");
        }

        bool exitRequested = false;

        while (!exitRequested)
        {
          
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Your Currency: ${userCurrency}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Options;");
            Console.WriteLine("1. Add product to cart");
            Console.WriteLine("2. List cart contents");
            Console.WriteLine("3. Calculate total cost");
            Console.WriteLine("4. View product information");
            Console.WriteLine("5. View available products");
            Console.WriteLine("5. Buy and Exit");
            Console.WriteLine("------------------------------");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
                continue;
            }
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the name of the product you want to purchase");
                    string productName = Console.ReadLine();
                    Product selectedProduct = availableproducts.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                    if (selectedProduct == null)
                    {
                        Console.WriteLine("Product not found.");
                    }
                    else if (selectedProduct.StockQuantity > 0 && userCurrency >= selectedProduct.Price)
                    {
                        cart.AddToCart(selectedProduct);
                        userCurrency -= selectedProduct.Price;
                        selectedProduct.StockQuantity--;
                        Console.WriteLine($"You added {selectedProduct.Name} for ${selectedProduct.Price} into your shopping cart.");

                        //Asking if they want to look at more products
                        Console.Write("Do you want to add something else? (Yes/No):");
                        if (Console.ReadLine().Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    else if (selectedProduct.StockQuantity == 0)
                    {
                        Console.WriteLine("Sorry this product is out of stock.");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you dont have enough money to buy this product.");
                    }
                    break;
                case 2:
                    cart.ListCartContent();
                    break;
                case 3:
                    decimal totalCost = cart.CalculateTotalCost();
                    Console.WriteLine($"Total cost of items in your cart: ${totalCost}");
                    break;
                case 4:
                    Console.WriteLine("Enter the name of the product to view details:");
                    string productToView = Console.ReadLine();
                    Product productInfo = availableproducts.Find(p => p.Name.Equals(productToView, StringComparison.OrdinalIgnoreCase));

                    if (productInfo != null)
                    {
                        productInfo.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }
                    break;
                case 5:
                    Console.WriteLine("Thank you for shopping with us!");
                    exitRequested = true;
                    break;

                case 6:
                    Console.WriteLine("Available Products:");

                    foreach (var product in availableproducts)
                    {
                        Console.WriteLine($"{product.Name} - ${product.Price} (In Stock: {product.StockQuantity})");
                    }
                    break;
                default:
                    Console.WriteLine("Pleace select valid option");
                    break;
            
            }
        }
    }
}