using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ybm.Infrastructure.Core.ExpressionHelper
{
    public class MyExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.Write("(");

            this.Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    Console.Write(" + ");
                    break;

                case ExpressionType.Divide:
                    Console.Write(" / ");
                    break;
            }

            this.Visit(node.Right);

            Console.Write(")");

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Console.Write(node.Value);
            //VisitBinary(node);
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Console.Write(node.Name);
            //VisitBinary(node);
            return node;
        }
    }
}
