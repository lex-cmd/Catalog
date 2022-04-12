namespace TestPath
{
				public class Catalog
    {
        public string?  path;
        private Flags flags;
        public Catalog(string? path)
        {
            this.path = path;
            flags = new Flags();
        }

        public Catalog()
        {
            path = null;
            flags = new Flags();
        }

        private struct Flags
        {
            public bool quite, path, output, humanread;

            public Flags()
            {
                quite = false;
                path = false;
                output = false;
                humanread = false;
            }
        }
        public bool SetFlag(char c)
        {
            bool res = false;

            switch (c)
            {
                case 'q':
                    flags.quite = true;
                    break;
                case 'p':
                    flags.path = true;
                    break;
                case 'o':
                    flags.output = true;
                    break;
                case 'h':
                    flags.humanread = true;
                    break;
            }
            return res;
        }

        public void PrintFlags()
        {
            Console.WriteLine("Quite = " + flags.quite + "\nPath = " + flags.path + "\nOutput = " + flags.output + "\nHumanread = " + flags.humanread);
								}

        public int Exec()
        {
            Console.WriteLine(path);
            if (!Directory.Exists(path))
            {
                Console.WriteLine(path + " path error");
                return (-1);
            }
            Console.WriteLine("-<" + path + ">");
            string[] subDirs = Directory.GetDirectories(path);
            foreach (string dir in subDirs)
            {
                Console.Write("--");
                Console.WriteLine(dir);
                if (Directory.Exists(dir))
                {
                    string[] subSubDirs = Directory.GetDirectories(dir);
                    foreach (string subSubDir in subSubDirs)
                    {
                        Console.Write("---");
                        Console.WriteLine(subSubDir);
                    }
                }
            }
            return (0);
        }
    }
}
