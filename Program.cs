using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextFile;
[assembly: InternalsVisibleTo("UnitTestPet")]


namespace assign2_HZ4BV8
{
	class Program
	{
		public class EmptyFileException : Exception { };
		public class EmptyListException : Exception { };
		public class NoMoodException : Exception { };	
		public class BadExhLevelException : Exception { };
		public class BadMoodException : Exception { };
		static void Main()
		{
			try
			{
				TextFileReader reader = new("input.txt");
				
				reader.ReadLine(out string line);
				if (line == null) 
				{
					throw new EmptyFileException();
				}
				int n = int.Parse(line);
				if (n == 0 )
				{
					throw new EmptyListException();					
				}

				List<Pet> pets = new();

				for (int i = 0; i < n; i++)
				{
					char[] separators = new char[] { ' ', '\t' };
					Pet? pet = null;

					if (reader.ReadLine(out line))
					{
						string[] tokens = line.Split(separators);
						char ch = char.Parse(tokens[0]);
						string name = tokens[1];
						try
						{
							int exhil = int.Parse(tokens[2]);
							if (exhil < 0 || exhil > 70)
							{
								throw new BadExhLevelException();
							}
							switch (ch)
							{
								case 'T': pet = new Tarantula(name, exhil); break;
								case 'H': pet = new Hamster(name, exhil); break;
								case 'C': pet = new Cat(name, exhil); break;
							}

							pets.Add(pet);
						}
						catch (BadExhLevelException)
						{
							Console.WriteLine("The exhiliration level can be only 0-70");
						}

					}
						
				}

				List<IMood> day = new();

				reader.ReadString(out line);
				int length = line.Length;

				if (line.Length <= 0)
				{
					throw new NoMoodException();
				}
				try
				{
					foreach (char ch in line)
					{
						switch (ch)
						{
							case 'u': day.Add(Usual.Instance()); break;
							case 'j': day.Add(Joyful.Instance()); break;
							case 'b': day.Add(Blue.Instance()); break;
							default: throw new BadMoodException();
						}
					}
				}
				catch (BadMoodException)
				{
					Console.WriteLine("The moods can be only of types (b, u, j)");
				}

				try
				{
					List<(string, int)> result = new();
					foreach (var p in pets)
					{
						result = p.MaxSearch(ref day, ref pets);
						Console.WriteLine(string.Join("\n", result));
						break;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("{0}", e.ToString());
				}
				
			}
			catch (EmptyFileException) 
			{
				Console.WriteLine("The file is empty");
			}
			catch (EmptyListException)
			{
				Console.WriteLine("Steve doesn't have pets");
			}
			catch (NoMoodException)
			{
				Console.WriteLine("Steve doesn' have any mood");
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("File was not found");
			}
		}	 
		
	}
	
}