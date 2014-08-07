using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starfish
{
	public interface ICommand
	{
	}

	public interface ICommand<T> : ICommand
	{
	}
}
