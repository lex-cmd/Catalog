using TestPath;


string? checkLine = null;

string[] argc = Environment.GetCommandLineArgs();

foreach(string arg in argc)
{
	checkLine += arg + " ";
}

if(checkLine != null)
	checkLine = checkLine.Trim();

Parser parser = new TestPath.Parser(checkLine);

Catalog? catalog = parser.GetNewCatalog();

if(catalog == null)
{
	Console.WriteLine("Parse error");
	return;
}
//catalog.PrintFlags();

try
{
	if(catalog.path != null)
		catalog.PrintPath(catalog.path, 0, catalog.GetDirectorySize(catalog.path));
	else
		Console.WriteLine(catalog.path + ": Error Path");

	if(checkLine != null)
		catalog.RecOutput(catalog.path);
	else
		Console.WriteLine("Error Output");
}
catch(Exception e)
{
	Console.WriteLine(e.Message);
}