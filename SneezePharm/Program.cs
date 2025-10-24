// See https://aka.ms/new-console-template for more information
using SneezePharm;

Console.WriteLine("Hello, World!");

VendaMedicamento v = new VendaMedicamento("1234567890");

Console.WriteLine(v.ToFile());