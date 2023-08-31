using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextFile;
[assembly: InternalsVisibleTo("UnitTestPet")]


namespace assign2_HZ4BV8
{
	interface IMood
	{
		int ChangeTar(Tarantula tarantula);
		int ChangeHam(Hamster hamster);
		int ChangeCat(Cat cat);
		IMood ChangeMood();
	}

	//class of usual mood
	class Usual : IMood
	{

		public int ChangeTar(Tarantula tarantula)
		{
			return tarantula.ExhLevel(-2);
		}

		public int ChangeHam(Hamster hamster)
		{
			return hamster.ExhLevel(-3);
		}

		public int ChangeCat(Cat cat)
		{
			return cat.ExhLevel(3);
		}

		private Usual() { }

		private static Usual? instance;
		public static Usual Instance()
		{
			if (instance == null)
			{
				instance = new Usual();
			}
			return instance;
		}

		public IMood ChangeMood()
		{
			return Joyful.Instance();
		}
	}


	//class of joyful mood
	class Joyful : IMood
	{
		public int ChangeTar(Tarantula tarantula)
		{
			return tarantula.ExhLevel(1);
		}

		public int ChangeHam(Hamster hamster)
		{
			return hamster.ExhLevel(2);
		}

		public int ChangeCat(Cat cat)
		{
			return cat.ExhLevel(3);
		}

		private Joyful() { }

		private static Joyful? instance;
		public static Joyful Instance()
		{
			if (instance == null)
			{
				instance = new Joyful();
			}
			return instance;
		}

		public IMood ChangeMood()
		{
			return Joyful.Instance();
		}
	}


	//class of blue mood
	class Blue : IMood
	{
		public int ChangeTar(Tarantula tarantula)
		{
			return tarantula.ExhLevel(-3);
		}

		public int ChangeHam(Hamster hamster)
		{
			return hamster.ExhLevel(-5);
		}

		public int ChangeCat(Cat cat)
		{
			return cat.ExhLevel(-7);
		}

		private Blue() { }

		private static Blue? instance;
		public static Blue Instance()
		{
			if (instance == null)
			{
				instance = new Blue();
			}
			return instance;
		}

		public IMood ChangeMood()
		{
			return Usual.Instance();
		}
	}
}
