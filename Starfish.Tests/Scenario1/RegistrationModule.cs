using System;

namespace Starfish.Tests.Scenario1
{
	class GetMessageCommand : ICommand<string>
	{

	}

	class GetLongMessageCommand : ICommand<string>
	{

	}

	class GetServerTimeCommand : ICommand<DateTime>
	{

	}

	class GetDiskSizeCommand : ICommand<long>
	{

	}

	class GetSumCommand : ICommand<int>
	{

	}

	class GetValueCommand : ICommand<int>
	{

	}

	static class RegistrationModule
	{
		/*
			// Concept of spaces? Can I send to a remote monitor?


			// Instead of using a name, all registrations should require one and only one command argument

			// Continuations also return using `Then`

			// How does one test this?

			// Single
			monitor.Register<GetMyIpCommand, string>(x => {

				return Then
					.InvokeAndReturn(new GetWebPage());

			}, continuation, exception);

			monitor.Register<GetMyIpCommand, string>(x => {

				return Then
					.Invoke(new GetWebPage())
					.Return("");

			}, exception);

			// Parallel
			monitor.Register<GetMyIpCommand, string>(x => {
				// Do it
				return Then
					.InvokeAndReturn(new[] {new Command().Timeout(TimeSpan.FromHours(1))});

			}, continuation, exception);

			// In order
			monitor.Register(() => {

				return Then
					.InvokeAndReturn(new[] {new Command()}, aggregator);

			}, continuation, exception);
		 */

		public static Monitor CreateMonitor()
		{
			var monitor = new Monitor();

			// Register handlers

			monitor.Register<GetMessageCommand, DateTime, string>(x => {

				// Do some work..

				return DeferredAction
					.InvokeAndReturn<DateTime>(new GetServerTimeCommand());
			}, x => {

				return DeferredAction
					.Return("The time is " + x.ToString());
			});

			monitor.Register<GetLongMessageCommand, Tuple<DateTime, long>, string>(x => {

				// Do some work..

				return DeferredAction
					.InvokeAndReturn<DateTime, long>(
						new GetServerTimeCommand(),
						new GetDiskSizeCommand());
			}, x => {

				return DeferredAction
					.Return("The time is " + x.Item1.ToString() + "And the space is " + x.Item2);
			});

			monitor.Register<GetSumCommand, int, int>(x => {

				// Do some work..

				return DeferredAction
					.Invoke(new GetValueCommand())
					.InvokeAndReturn<int, int>(
					new ICommand[] { new GetValueCommand(), new GetValueCommand() },
					(a, val) => a + val
				);

			}, x => {

				return DeferredAction
					.Return(x);
			});


			return monitor;
		}
	}

	//public class Tester
	//{
	//	public class Cmd<T>
	//	{
	//
	//	}
	//
	//	public R InvokeAndReturn<C, R>(C c)
	//		where C : Cmd<R>
	//		//where T : 
	//	{
	//		return default(R);
	//	}
	//
	//	public void Sample()
	//	{
	//		var cmd = new Cmd<int>();
	//
	//		var r = InvokeAndReturn(cmd);
	//	}
	//}
}
