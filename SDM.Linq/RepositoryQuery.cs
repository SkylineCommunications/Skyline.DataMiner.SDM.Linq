namespace Skyline.DataMiner.SDM.Linq
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public class RepositoryQuery<T> : IOrderedQueryable<T>
		where T : class
    {
		public RepositoryQuery(RepositoryQueryProvider<T> repository)
		{
			Repository = repository ?? throw new ArgumentNullException(nameof(repository));
			Expression = Expression.Constant(this);
		}

		public RepositoryQuery(RepositoryQueryProvider<T> provider, Expression expression)
		{
			Repository = provider ?? throw new ArgumentNullException(nameof(provider));
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		public Type ElementType => typeof(T);

		public Expression Expression { get; }

		public RepositoryQueryProvider<T> Repository { get; }

		IQueryProvider IQueryable.Provider => Repository;

		public IEnumerator<T> GetEnumerator()
		{
			return Repository.Execute<IEnumerable<T>>(Expression).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
