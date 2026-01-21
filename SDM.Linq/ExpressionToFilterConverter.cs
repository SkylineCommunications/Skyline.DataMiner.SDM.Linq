namespace Skyline.DataMiner.SDM.Linq
{
	using System;
	using System.Linq.Expressions;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;

	internal class ExpressionToFilterConverter<T>
		where T : class
	{
		private readonly RepositoryQueryProvider<T> _queryProvider;
		private readonly string _basePath = String.Empty;

		private ExpressionToFilterConverter(RepositoryQueryProvider<T> queryProvider)
		{
			_queryProvider = queryProvider ?? throw new ArgumentNullException(nameof(queryProvider));
		}

		private ExpressionToFilterConverter(RepositoryQueryProvider<T> queryProvider, string basePath)
			: this(queryProvider)
		{
			_basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
		}

		public static FilterElement<T> Convert(Expression expression, RepositoryQueryProvider<T> queryProvider)
		{
			if (expression is null)
			{
				throw new ArgumentNullException(nameof(expression));
			}

			if (queryProvider is null)
			{
				throw new ArgumentNullException(nameof(queryProvider));
			}

			var converter = new ExpressionToFilterConverter<T>(queryProvider);

			return converter.ConvertInternal(expression);
		}

		private FilterElement<T> ConvertInternal(Expression expr)
		{
			return expr switch
			{
				BinaryExpression binary => ConvertBinary(binary),
				UnaryExpression unary => ConvertUnary(unary),
				ConstantExpression constant => ConvertConstant(constant),
				MethodCallExpression methodCall => ConvertMethodCall(methodCall),
				LambdaExpression lambda => ConvertInternal(lambda.Body),
				MemberExpression member => ConvertMember(member),
				_ => throw new NotSupportedException($"Unsupported expression: {expr.NodeType} ({expr})"),
			};
		}

		private FilterElement<T> ConvertBinary(BinaryExpression node)
		{
			// Handle logical combinations
			if (node.NodeType == ExpressionType.AndAlso || node.NodeType == ExpressionType.OrElse)
			{
				var left = ConvertInternal(node.Left);
				var right = ConvertInternal(node.Right);

				return node.NodeType switch
				{
					ExpressionType.AndAlso => new ANDFilterElement<T>(left, right),
					ExpressionType.OrElse => new ORFilterElement<T>(left, right),
					_ => throw new InvalidOperationException()
				};
			}

			// Handle comparisons
			// Handle x.Age < 25
			if (TryGetFullMemberPath(node.Left, out var path) && ExpressionTools.TryGetValue(node.Right, out var value))
			{
				var comparer = ExpressionTypeToComparer(node.NodeType);
				return _queryProvider.CreateFilter(path, comparer, value);
			}

			// Handle 25 > x.Age, is the same as x.Age <= 25
			if (TryGetFullMemberPath(node.Right, out path) && ExpressionTools.TryGetValue(node.Left, out value))
			{
				var comparer = ExpressionTypeToComparer(node.NodeType);
				if (comparer == Comparer.LT || comparer == Comparer.LTE || comparer == Comparer.GT || comparer == Comparer.GTE)
				{
					comparer = comparer.Invert();
				}

				return _queryProvider.CreateFilter(path, comparer, value);
			}

			throw new NotSupportedException($"Unsupported comparison expression: {node}");
		}

		private FilterElement<T> ConvertUnary(UnaryExpression node)
		{
			switch (node.NodeType)
			{
				case ExpressionType.Not:
					var operand = ConvertInternal(node.Operand);
					return new NOTFilterElement<T>(operand);

				case ExpressionType.Quote:
					return ConvertInternal(node.Operand);

				default:
					throw new NotSupportedException($"Unsupported unary expression: {node.NodeType}");
			}
		}

		private FilterElement<T> ConvertConstant(ConstantExpression node)
		{
			return node.Value switch
			{
				true => new TRUEFilterElement<T>(),
				false => new FALSEFilterElement<T>(),
				_ => throw new NotSupportedException($"Unsupported constant: {node.Value}")
			};
		}

		private FilterElement<T> ConvertMethodCall(MethodCallExpression node)
		{
			// Handle .Where(x => x.Name.Contains("test"))
			if (node.Method.DeclaringType == typeof(String) &&
				node.Method.Name == nameof(String.Contains) &&
				TryGetFullMemberPath(node.Object, out var path) &&
				ExpressionTools.TryGetValue(node.Arguments[0], out var value))
			{
				return _queryProvider.CreateFilter(path, Comparer.Contains, value);
			}

			// The below will work once platform allows regex filtering in queries

			////// Handle .Where(x => x.Name.StartsWith("test"))
			////if (node.Method.DeclaringType == typeof(String) &&
			////	node.Method.Name == nameof(String.StartsWith) &&
			////	TryGetFullMemberPath(node.Object, out path) &&
			////	ExpressionTools.TryGetValue(node.Arguments[0], out value))
			////{
			////	return _queryProvider.CreateFilter(path, Comparer.Regex, $"^{value}");
			////}

			////// Handle .Where(x => x.Name.EndsWith("test"))
			////if (node.Method.DeclaringType == typeof(String) &&
			////	node.Method.Name == nameof(String.EndsWith) &&
			////	TryGetFullMemberPath(node.Object, out path) &&
			////	ExpressionTools.TryGetValue(node.Arguments[0], out value))
			////{
			////	return _queryProvider.CreateFilter(path, Comparer.Regex, $"{value}$");
			////}

			// Handle .Where(x => x.Levels.Any(l => l.Endpoint == videoSource1))
			if (node.Method.DeclaringType == typeof(System.Linq.Enumerable) &&
				node.Method.Name == nameof(System.Linq.Enumerable.Any) &&
				node.Arguments.Count == 2 &&
				TryGetFullMemberPath(node.Arguments[0], out var collectionPath))
			{
				// Create a nested query provider with the collection path as base
				return new ExpressionToFilterConverter<T>(_queryProvider, collectionPath).ConvertInternal(node.Arguments[1]);
			}

			// Handle .Where(x => x.Property.Equals(value))
			if (node.Method.Name == nameof(object.Equals) && (
				(TryGetFullMemberPath(node.Object, out path) && ExpressionTools.TryGetValue(node.Arguments[0], out value)) ||
				(TryGetFullMemberPath(node.Arguments[0], out path) && ExpressionTools.TryGetValue(node.Object, out value))))
			{
				return _queryProvider.CreateFilter(path, Comparer.Equals, value);
			}

			throw new NotSupportedException($"Unsupported method call: {node.Method}");
		}

		private FilterElement<T> ConvertMember(MemberExpression node)
		{
			// Handle .Where(x => x.IsActive)
			if (node.Type == typeof(Boolean) && TryGetFullMemberPath(node, out var path))
			{
				return _queryProvider.CreateFilter(path, Comparer.Equals, true);
			}

			throw new NotSupportedException($"Unsupported property expression: {node}");
		}

		private static Comparer ExpressionTypeToComparer(ExpressionType type)
		{
			return type switch
			{
				ExpressionType.Equal => Comparer.Equals,
				ExpressionType.NotEqual => Comparer.NotEquals,
				ExpressionType.LessThan => Comparer.LT,
				ExpressionType.LessThanOrEqual => Comparer.LTE,
				ExpressionType.GreaterThan => Comparer.GT,
				ExpressionType.GreaterThanOrEqual => Comparer.GTE,
				_ => throw new NotSupportedException($"Unknown comparison: {type}")
			};
		}

		private bool TryGetFullMemberPath(Expression expression, out string fullPath)
		{
			fullPath = String.Empty;
			if (!ExpressionTools.TryGetMemberPath(expression, out var path))
			{
				return false;
			}

			fullPath = GetFullPath(path);
			return true;
		}

		private string GetFullPath(string path)
		{
			if (String.IsNullOrEmpty(_basePath))
			{
				return path;
			}

			return String.Join(".", _basePath, path);
		}
	}
}
