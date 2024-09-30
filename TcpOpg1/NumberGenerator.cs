using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpOpg1
{
	public class NumberGenerator
	{
		private static Random _random = new Random();

		public static int Generate(int minimum, int maximum)
		{
			return _random.Next(minimum, maximum + 1); // the +1 is so the max nimber can be the given maximum
		}
	}
}