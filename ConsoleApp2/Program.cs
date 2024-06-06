using System;
using System.Collections.Generic;

public class Invoice
{
    public List<string> Items { get; set; }
    public List<int> Quantities { get; set; }
    public List<double> UnitPrices { get; set; }
    public double Total { get; set; }
}

public class Program
{
    static Random random = new Random();

    public static void Main()
    {
        Console.Write("Enter the number of invoices you want to generate (up to 1000): ");
        int numberOfInvoices;
        while (!int.TryParse(Console.ReadLine(), out numberOfInvoices) || numberOfInvoices <= 0 || numberOfInvoices > 1000)
        {
            Console.Write("Please enter a valid positive number (up to 1000): ");
        }

        List<Invoice> invoices = GenerateInvoices(numberOfInvoices, 55, 590);

        foreach (var invoice in invoices)
        {
            Console.WriteLine("Invoice:");
            for (int i = 0; i < invoice.Items.Count; i++)
            {
                Console.WriteLine($"  Item: {invoice.Items[i]}, Quantity: {invoice.Quantities[i]}, Unit Price: {invoice.UnitPrices[i]}, Total: {invoice.Quantities[i] * invoice.UnitPrices[i]}");
            }
            Console.WriteLine($"Total: {invoice.Total}");
            Console.WriteLine();
        }
    }

    public static List<Invoice> GenerateInvoices(int numberOfInvoices, double minTotal, double maxTotal)
    {
        List<Invoice> invoices = new List<Invoice>();
        string[] items = { "Voltarin", "Comatrin", "Vivolin" };
        double[] unitPrices = { 10, 15, 22 };
        int[] maxQuantities = { 2500, 101, 550 };

        for (int i = 0; i < numberOfInvoices; i++)
        {
            Invoice invoice = new Invoice
            {
                Items = new List<string>(),
                Quantities = new List<int>(),
                UnitPrices = new List<double>(),
                Total = 0
            };

            int numItems = random.Next(1, 4); // Randomly choose between 1, 2, or 3 items

            while (invoice.Items.Count < numItems || invoice.Total < minTotal || invoice.Total > maxTotal)
            {
                invoice.Items.Clear();
                invoice.Quantities.Clear();
                invoice.UnitPrices.Clear();
                invoice.Total = 0;

                for (int j = 0; j < numItems; j++)
                {
                    int index = random.Next(items.Length);
                    string item = items[index];
                    int maxQuantity = (int)(maxTotal / unitPrices[index]);
                    int quantity = random.Next(1, maxQuantity + 1);

                    double itemTotal = quantity * unitPrices[index];
                    if (invoice.Total + itemTotal > maxTotal)
                    {
                        continue;
                    }

                    invoice.Items.Add(item);
                    invoice.Quantities.Add(quantity);
                    invoice.UnitPrices.Add(unitPrices[index]);
                    invoice.Total += itemTotal;
                }
            }

            invoices.Add(invoice);
        }

        return invoices;
    }
}
