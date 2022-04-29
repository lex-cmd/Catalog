namespace TestPath
{
	public class Catalog
	{
		public string? path { get; set; }

		public string? outFile { get; set; }

		public bool ShowAll { get; private set; }

		private Flags flags;
		private static int maxDepth { get; } = int.MaxValue;

		public Catalog(string path)
		{
			this.path = path;
			flags = new Flags();
			outFile = outFile = Directory.GetCurrentDirectory() + "\\sizes " + DateTime.Today.ToString().Split(" ")[0] + ".txt";
		}

		private struct Flags
		{
			public bool quite, path, output, humanread;
		}
		public bool SetFlag(string str)
		{

			if(str == "-q" || str == "--quite")
				flags.quite = true;
			else if(str == "-p" || str == "--path")
				flags.path = true;
			else if(str == "-o" || str == "--output")
				flags.output = true;
			else if(str == "-h" || str == "--humanread")
				flags.humanread = true;
			else
				return false;
			return true;
		}

		public void PrintFlags()
		{
			Console.WriteLine("Quite = " + flags.quite + "\nPath = "
															+ flags.path + "\nOutput = " + flags.output
															+ "\nHumanread = " + flags.humanread);
		}

		public void RecOutput(string? path, int depth = 1)
		{
			if(depth >= maxDepth)
				return;

			if(string.IsNullOrWhiteSpace(path))
				return;

			DirectoryInfo dirInfo = new(path);
			var fileSystemInfo = dirInfo.GetFileSystemInfos()
							.Where(f => ShowAll || !f.Name.StartsWith("."))
							.OrderBy(f => f.Name)
							.ToList();

			foreach(var fsItem in fileSystemInfo.Take(fileSystemInfo.Count))
			{
				PrefixPrint(depth, "--");

				FileAttributes attr = File.GetAttributes(fsItem.FullName);


				if(attr.HasFlag(FileAttributes.Directory))
				{
					PrintPath(fsItem.Name, 1, GetDirectorySize(fsItem.FullName));
					RecOutput(fsItem.FullName, depth + 1);
				}
				else
				{
					FileInfo file = new(fsItem.FullName);
					PrintPath(fsItem.Name, 1, (ulong)file.Length);
				}

			}
			return;
		}

		public void PrintPath(string name, int type, ulong sizeToPrint)
		{
			string finalPrint = name;

			if(type == 0)
				finalPrint = "-<" + name + ">";

			if(flags.humanread && sizeToPrint > 1024)
				finalPrint += getHumanReadSize(sizeToPrint);
			else
				finalPrint += " (" + sizeToPrint + ")";
			if(flags.quite || flags.quite && outFile != null)
				File.AppendAllTextAsync(outFile, finalPrint + "\n");
			else
				Console.WriteLine(finalPrint);
		}

		private string getHumanReadSize(ulong sizeToPrintInBytes)
		{
			string[] byteUnits = { " KB", " MB", " GB", " TB", " PB", " EB", " ZB", " YB" };
			int i = 0;

			while(sizeToPrintInBytes > 1024)
			{
				sizeToPrintInBytes /= 1024;
				i++;
			}

			return " (" + Math.Round(Math.Max(sizeToPrintInBytes, 0.1), 2) + byteUnits[i] + ")";
		}

		private void PrefixPrint(int depth, string v)
		{
			try
			{
				for(int i = 0; i < depth; i++)
				{
					if(flags.quite && outFile != null)
						File.AppendAllTextAsync(outFile, v);
					else
						Console.Write(v);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public ulong GetDirectorySize(string dir)
		{
			ulong startDirectorySize = 0;
			if(!Directory.Exists(dir))
				return startDirectorySize;

			var currentDirectory = new DirectoryInfo(dir);

			currentDirectory.GetFiles().ToList().ForEach(f => startDirectorySize += (ulong)f.Length);

			currentDirectory.GetDirectories().ToList()
							.ForEach(d => startDirectorySize += GetDirectorySize(d.FullName));

			return startDirectorySize;
		}
	}
}
