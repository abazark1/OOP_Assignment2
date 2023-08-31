using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextFile;
using static assign2_HZ4BV8.Program;
[assembly: InternalsVisibleTo("UnitTestPet")]


namespace assign2_HZ4BV8
{
	class EmptyFileException : Exception { };
	class EmptyListException : Exception { };
	class NoMoodException : Exception { };
	class BadExhLevelException : Exception { };
	class BadMoodException : Exception { };


	class FileHandle
	{
		public List<Pet> PopulatePets(string filename)
		{
			TextFileReader reader = new(filename);

			reader.ReadLine(out string line);
			if (line == null)
			{
				throw new EmptyFileException();
			}
			int n = int.Parse(line);
			if (n == 0)
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

			}
			return pets;
		}

		public List<IMood> PopulateMood(string filename) 
		{

			TextFileReader reader = new(filename);

			reader.ReadLine(out string line);
			int n = int.Parse(line);
			if (n == 0)
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
					int exhil = int.Parse(tokens[2]);

					switch (ch)
					{
						case 'T': pet = new Tarantula(name, exhil); break;
						case 'H': pet = new Hamster(name, exhil); break;
						case 'C': pet = new Cat(name, exhil); break;
					}
				}
				pets.Add(pet);
			}

			List<IMood> day = new();

			reader.ReadString(out line);
			int length = line.Length;

			if (line.Length <= 0)
			{
				throw new NoMoodException();
			}

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
			return day;
		}
	}

}