using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPath;

namespace TestPath
{
				public class Parser
				{
								string? rawString;
								Catalog catalog;

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
												Catalog catalog = new Catalog();
												catalog.path = Directory.GetCurrentDirectory();
												Console.WriteLine("Current directory: " + catalog.path);

												Console.WriteLine("rawString = " + rawString);
												if (rawString == null || rawString == "")
												{
																Console.WriteLine("Error: rawString");
																return null;
												}

												/*Console.WriteLine("rawString = " + rawString);
												Console.WriteLine(rawString.Length);*/
												for (int i = 0; i < rawString.Length; i++)
												{
																Console.WriteLine("pass");
																if (rawString[i] == '-' && !char.IsWhiteSpace(rawString[i + 1]))
																{
																				catalog.SetFlag(rawString[i + 1]);
																}
																Console.WriteLine();
												}
												return catalog;
								}
				}
}
