using Expressive.Expressions;

namespace Expressive.Operators.Relational
{
	internal class GreaterThanOperator : OperatorBase
	{
		public override string[] Tags => new string[1]
		{
			">"
		};

		public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
		{
			return new BinaryExpression(BinaryExpressionType.GreaterThan, expressions[0], expressions[1], options);
		}

		public override OperatorPrecedence GetPrecedence(Token previousToken)
		{
			return OperatorPrecedence.GreaterThan;
		}
	}
}
