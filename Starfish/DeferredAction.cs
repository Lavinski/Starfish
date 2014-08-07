using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Starfish
{
	public static class DeferredAction
	{
		public static ReturnableBuilder Invoke(ICommand command)
		{
			return new ReturnableBuilder(new SingularInvocation(command));
		}

		public static Returnable<T> InvokeAndReturn<T>(ICommand command)
		{
			return new Returnable<T>(new SingularInvocation(command));
		}

		public static Returnable<Tuple<TR1, TR2>> InvokeAndReturn<TR1, TR2>(ICommand command1, ICommand command2)
		{
			return new Returnable<Tuple<TR1, TR2>>(new ParallelInvocation(command1, command2));
		}

		public static Returnable<TR> InvokeAndReturn<T, TR>(IEnumerable<ICommand> command, Func<TR, T, TR> aggregator)
		{
			return new Returnable<TR>(new AggregateInvocation<T, TR>(command, aggregator));
		}

		public static Returnable<T> Return<T>(T value)
		{
			return new Returnable<T>(value);
		}

		public static Returnable Return()
		{
			return new Returnable();
		}
	}

	public class ReturnableBuilder
	{
		private readonly IImmutableList<Invocation> invocations;

		internal ReturnableBuilder(Invocation invocation)
		{
			invocations = ImmutableList.Create(invocation);
		}

		internal ReturnableBuilder(ReturnableBuilder builder, Invocation invocation)
		{
			invocations = builder.invocations.Add(invocation);
		}

		public ReturnableBuilder Invoke(ICommand command)
		{
			return new ReturnableBuilder(this, new SingularInvocation(command));
		}

		public Returnable<T> InvokeAndReturn<T>(ICommand command)
		{
			return new Returnable<T>(new SingularInvocation(command));
		}

		public Returnable<Tuple<TR1, TR2>> InvokeAndReturn<TR1, TR2>(ICommand command1, ICommand command2)
		{
			return new Returnable<Tuple<TR1, TR2>>(new ParallelInvocation(command1, command2));
		}

		public Returnable<TR> InvokeAndReturn<T, TR>(IEnumerable<ICommand> command, Func<TR, T, TR> aggregator)
		{
			return new Returnable<TR>(new AggregateInvocation<T, TR>(command, aggregator));
		}

		public Returnable<T> Return<T>(T value)
		{
			return new Returnable<T>(value);
		}

		public Returnable Return()
		{
			return new Returnable();
		}
	}

	public interface IReturnable
	{
		IImmutableList<Invocation> Invocations { get; }
	}

	public class Returnable : IReturnable
	{
		private readonly IImmutableList<Invocation> invocations;

		public IImmutableList<Invocation> Invocations
		{
			get { return invocations; }
		}

		internal Returnable()
		{
			invocations = ImmutableList<Invocation>.Empty;
		}

		internal Returnable(Invocation invocation)
		{
			invocations = ImmutableList.Create(invocation);
		}

		internal Returnable(IEnumerable<Invocation> invocations)
		{
			this.invocations = ImmutableList.CreateRange(invocations);
		}

		internal Returnable(IReturnable returnable, Invocation invocation)
		{
			invocations = returnable.Invocations.Add(invocation);
		}

	}
	
	public interface IReturnable<out T>
	{
		T Value { get; }
		bool HasValue { get; }
	}

	public class Returnable<T> : Returnable, IReturnable<T>
	{
		private readonly T value;
		private readonly bool hasValue = false;

		internal Returnable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		internal Returnable(Invocation invocation)
			: base(invocation)
		{
			this.value = default(T);
		}

		internal Returnable(IEnumerable<Invocation> invocations)
			: base(invocations)
		{
			this.value = default(T);
		}

		internal Returnable(IReturnable returnable, Invocation invocation)
			: base(returnable, invocation)
		{
			this.value = default(T);
		}

		public T Value
		{
			get { return value; }
		}

		public bool HasValue
		{
			get { return hasValue; }
		}
	}

	public class Invocation
	{
		// Timeout? or do I put this on a command
	}

	public class SingularInvocation : Invocation
	{
		public SingularInvocation(ICommand command)
		{
			throw new NotImplementedException();
		}
	}

	public class AggregateInvocation<T, TR> : Invocation
	{
		// Max Concurrency

		public AggregateInvocation(IEnumerable<ICommand> command, Func<TR, T, TR> aggregator) : base()
		{
			throw new NotImplementedException();
		}
	}

	public class ParallelInvocation : Invocation
	{
		// Max Concurrency

		public ParallelInvocation(ICommand command1, ICommand command2) : base()
		{
			throw new NotImplementedException();
		}
	}
}
