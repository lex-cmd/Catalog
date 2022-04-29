namespace TestPath
{
 public class Parser
 {
  private string? rawString { get; set; }

  public Parser(string? rawString)
  {
   this.rawString = rawString;
  }
  public Parser()
  {
   this.rawString = null;
  }

  public Catalog GetNewCatalog()
  {
   Catalog newCatalog = new Catalog(Directory.GetCurrentDirectory());

   if(String.IsNullOrWhiteSpace(rawString))
   {
	Console.WriteLine("Parse error");

	return null;
   }

   string[]? splitStr = rawString.Split(" ");

   for(int i = 0; i < splitStr.Length; i++)
   {
	if(!String.IsNullOrWhiteSpace(splitStr[i]))
	{
	 if(splitStr[i].StartsWith('-'))
	  if(!newCatalog.SetFlag(splitStr[i]))
	   Console.WriteLine("Invalid Flag");
	 try
	 {
	  if(splitStr[i] == "-p" && splitStr[i + 1] != null)
	   newCatalog.path = splitStr[i + 1];
	  if(splitStr[i] == "-o" && splitStr[i + 1] != null)
	  {
	   newCatalog.outFile = splitStr[i + 1] + "\\sizes " + DateTime.Today.ToString().Split(" ")[0] + ".txt";
	   if(File.Exists(newCatalog.outFile))
		File.Delete(newCatalog.outFile);
	  }
	 }
	 catch
	 {
	  Console.WriteLine("Argument error exception");
	 }
	}
   }
   return newCatalog;
  }
 }

}