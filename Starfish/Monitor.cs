using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Starfish
{
	public class Monitor
	{
		public Monitor()
		{
			// Start the background thread..
		}

		public Task<T> Send<T>(ICommand command)
		{
			throw new NotImplementedException();
		}

		public void SendForever(ICommand command)
		{
			throw new NotImplementedException();
		}

		public Task<T> Send<T>(Space space, ICommand command)
		{
			throw new NotImplementedException();
		}

		public void SendForever(Space space, ICommand command)
		{
			throw new NotImplementedException();
		}

		private MultiValueDictionary<Type, Registration> registrations = new MultiValueDictionary<Type, Registration>();

		public void Register<TCommand, TReturn>(Func<TCommand, IReturnable<TReturn>> function)
			where TCommand : ICommand<TReturn>
		{
			throw new NotImplementedException();
		}

		public void Register<TCommand, TReturn>(Func<TCommand, IReturnable<TReturn>> function, Func<Exception, IReturnable<TReturn>> exception)
			where TCommand : ICommand<TReturn>
		{
			throw new NotImplementedException();
		}

		public void Register<TCommand, TCont, TReturn>(Func<TCommand, IReturnable<TCont>> function, Func<TCont, IReturnable<TReturn>> continuation)
			where TCommand : ICommand<TReturn>
		{
			throw new NotImplementedException();
		}

		public void Register<TCommand, TCont, TReturn>(Func<TCommand, IReturnable<TCont>> function, Func<TCont, IReturnable<TReturn>> continuation, Func<Exception, IReturnable<TReturn>> exception)
			where TCommand : ICommand<TReturn>
			where TCont : class
			where TReturn : class
		{
			var registration = new Registration(x => function((TCommand)x), x => continuation((TCont)x), exception);

			registrations.Add(typeof(TCommand), registration);
		}
	}

	internal class Registration
	{
		private readonly Func<object, IReturnable<object>> function;
		private readonly Func<object, IReturnable<object>> continuation;
		private readonly Func<Exception, IReturnable<object>> exception;

		public Registration(Func<object, IReturnable<object>> function, Func<object, IReturnable<object>> continuation, Func<Exception, IReturnable<object>> exception)
		{
			this.function = function;
			this.continuation = continuation;
			this.exception = exception;
		}

		public Func<object, IReturnable<object>> Function
		{
			get { return function; }
		}

		public Func<object, IReturnable<object>> Continuation
		{
			get { return continuation; }
		}

		public Func<Exception, IReturnable<object>> Exception
		{
			get { return exception; }
		}
	}
}
