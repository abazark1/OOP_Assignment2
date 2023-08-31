using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextFile;
[assembly: InternalsVisibleTo("UnitTestPet")]

namespace assign2_HZ4BV8
{
	abstract class Pet
	{
		protected string name;
		protected int exhiliration;
		protected abstract int Raise(IMood m);

		public string GetName() { return name; }
		public int GetExhiliration() { return exhiliration; }

		public bool Alive() { return exhiliration > 0; }
		public int ExhLevel(int l)
		{
			exhiliration += l;
			return exhiliration;
		}

		public Pet(string n, int e)
		{
			name = n;
			exhiliration = e;
		}

		public bool AllHappy(ref List<Pet> pets)
		{
			bool l = true;
			for (int i = 0; i < pets.Count; i++)
			{
				l = l && pets[i].GetExhiliration() >= 5;
			}
			return l;
		}

		public IMood IncreaseMood(ref List<Pet> pets, IMood m)
		{
			if (AllHappy(ref pets))
			{
				m = m.ChangeMood();
				return m;
			}
			else
			{
				return m;
			}
		}

		public List<List<(string, int)>> GetMatrix(ref List<IMood> moods, ref List<Pet> pets)
		{
			List<List<(string, int)>> matrix = new();

			for (int i = 0; i < moods.Count; i++)
			{
				List<(string, int)> innerList = new();
				IMood newm = IncreaseMood(ref pets, moods[i]);
				for (int j = 0; j < pets.Count; j++)
				{
					if (pets[j].Alive())
					{
						var myTuple = (pets[j].GetName(), pets[j].Raise(newm));
						if (myTuple.Item2 > 70)
						{
							innerList.Add((pets[j].GetName(), 70));
						}
						else
						{
							innerList.Add(myTuple);
						}
					}
					else
					{
						var myTuple = (pets[j].GetName(), 0);
						innerList.Add(myTuple);
					}
				}
				Console.WriteLine(string.Join(" ", innerList));

				matrix.Add(innerList);
			}
			return matrix;
		}

		public List<(string, int)> MaxSearch(ref List<IMood> moods, ref List<Pet> pets)
		{
			List<(string, int)> result = new();

			List<List<(string, int)>> matrix = GetMatrix(ref moods, ref pets);


			for (int i = 0; i < moods.Count; i++)
			{
				(string, int) max = matrix[i][0];
				for (int j = 1; j < pets.Count; j++)
				{
					if (matrix[i][j].Item2 > max.Item2)
					{
						max = matrix[i][j];
					}
				}
				result.Add(max);
			}

			//Console.WriteLine(string.Join("\n", result));

			return result;
		}

	}
	

	//class of Tarantulas
	class Tarantula : Pet
	{
		public Tarantula(string n, int e) : base(n, e) { }
		protected override int Raise(IMood m)
		{
			return m.ChangeTar(this);
		}
	}

	//class of Hamsters
	class Hamster : Pet
	{
		public Hamster(string n, int e) : base(n, e) { }
		protected override int Raise(IMood m)
		{
			return m.ChangeHam(this);
		}
	}

	//class of Cats
	class Cat : Pet
	{
		public Cat(string n, int e) : base(n, e) { }
		protected override int Raise(IMood m)
		{
			return m.ChangeCat(this);
		}
	}
}

