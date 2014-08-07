using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Starfish.Tests.Scenario1;

namespace Starfish.Tests
{
	[TestClass]
	public class UnitTest1
	{


		[TestMethod]
		public void TestMethod1()
		{
			var monitor = RegistrationModule.CreateMonitor();

			monitor.Send<string>(new GetMessageCommand);

		}
	}
}
