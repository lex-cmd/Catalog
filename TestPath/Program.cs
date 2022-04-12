using TestPath;


string? path = Console.ReadLine();

Parser parser = new TestPath.Parser(path);

TestPath.Catalog catalog = parser.GetNewCatalog();
catalog.PrintFlags();
