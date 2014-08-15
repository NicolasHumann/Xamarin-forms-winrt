using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Windows.UI.Xaml;

namespace Xamarin.Forms.Platform.WinRT
{
    public static class Forms
    {
        internal static bool IsInitialized;

        public static void Init()
        {
            if (IsInitialized)
                return;

            Device.PlatformServices = new PlatformServices();

            Registrar.RegisterAll(new[]
                                  {
                                      typeof (ExportRendererAttribute),
                                      typeof (ExportImageSourceHandlerAttribute),
                                      typeof (ExportCellAttribute)
                                  });
            Device.OS = TargetPlatform.Other;
            Device.Idiom = TargetIdiom.Tablet;

            string name = typeof(Forms).GetType().Name;
            var myResourceDictionary = new Windows.UI.Xaml.ResourceDictionary
                                       {
                                           Source =
                                               new Uri("ms-appx:///Xamarin.Forms.Platform.WP8/WinRTResources.xaml",
                                               UriKind.RelativeOrAbsolute)
                                       };

            Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
            //  ExpressionSearch.Default = new Forms.WinPhoneExpressionSearch();

            IsInitialized = true;
        }

        public static UIElement ConvertPageToUIElement(this Page page, Windows.UI.Xaml.Controls.Page applicationPage)
        {
            Platform platform = new Platform(applicationPage);
            platform.SetPage(page);
            return platform;
        }

        private sealed class WinPhoneExpressionSearch : IExpressionSearch
        {
            private List<object> results;

            private Type targeType;

            public WinPhoneExpressionSearch()
            {
            }

            public List<T> FindObjects<T>(Expression expression)
            where T : class
            {
                this.results = new List<object>();
                this.targeType = typeof(T);
                this.Visit(expression);
                return this.results.Select(o => o as T).ToList();

            }

            private void Visit(Expression expression)
            {
                if (expression == null)
                {
                    return;
                }
                switch (expression.NodeType)
                {
                    case ExpressionType.Add:
                    case ExpressionType.AddChecked:
                    case ExpressionType.And:
                    case ExpressionType.AndAlso:
                    case ExpressionType.ArrayIndex:
                    case ExpressionType.Coalesce:
                    case ExpressionType.Divide:
                    case ExpressionType.Equal:
                    case ExpressionType.ExclusiveOr:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.LeftShift:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.Modulo:
                    case ExpressionType.Multiply:
                    case ExpressionType.MultiplyChecked:
                    case ExpressionType.NotEqual:
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                    case ExpressionType.Power:
                    case ExpressionType.RightShift:
                    case ExpressionType.Subtract:
                    case ExpressionType.SubtractChecked:
                        {
                            BinaryExpression binaryExpression = (BinaryExpression)expression;
                            this.Visit(binaryExpression.Left);
                            this.Visit(binaryExpression.Right);
                            this.Visit(binaryExpression.Conversion);
                            return;
                        }
                    case ExpressionType.ArrayLength:
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                    case ExpressionType.Negate:
                    case ExpressionType.UnaryPlus:
                    case ExpressionType.NegateChecked:
                    case ExpressionType.Not:
                    case ExpressionType.Quote:
                    case ExpressionType.TypeAs:
                        {
                            this.Visit(((UnaryExpression)expression).Operand);
                            return;
                        }
                    case ExpressionType.Call:
                        {
                            MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
                            this.Visit(methodCallExpression.Object);
                            Forms.WinPhoneExpressionSearch.VisitList<Expression>(methodCallExpression.Arguments, new Action<Expression>(this.Visit));
                            return;
                        }
                    case ExpressionType.Conditional:
                        {
                            ConditionalExpression conditionalExpression = (ConditionalExpression)expression;
                            this.Visit(conditionalExpression.Test);
                            this.Visit(conditionalExpression.IfTrue);
                            this.Visit(conditionalExpression.IfFalse);
                            return;
                        }
                    case ExpressionType.Constant:
                        {
                            return;
                        }
                    case ExpressionType.Invoke:
                        {
                            InvocationExpression invocationExpression = (InvocationExpression)expression;
                            Forms.WinPhoneExpressionSearch.VisitList<Expression>(invocationExpression.Arguments, new Action<Expression>(this.Visit));
                            this.Visit(invocationExpression.Expression);
                            return;
                        }
                    case ExpressionType.Lambda:
                        {
                            this.Visit(((LambdaExpression)expression).Body);
                            return;
                        }
                    case ExpressionType.ListInit:
                        {
                            ListInitExpression listInitExpression = (ListInitExpression)expression;
                            Forms.WinPhoneExpressionSearch.VisitList<Expression>(listInitExpression.NewExpression.Arguments, new Action<Expression>(this.Visit));
                            Forms.WinPhoneExpressionSearch.VisitList<ElementInit>(listInitExpression.Initializers, (ElementInit initializer) => Forms.WinPhoneExpressionSearch.VisitList<Expression>(initializer.Arguments, new Action<Expression>(this.Visit)));
                            return;
                        }
                    case ExpressionType.MemberAccess:
                        {
                            this.VisitMemberAccess((MemberExpression)expression);
                            return;
                        }
                    case ExpressionType.MemberInit:
                        {
                            MemberInitExpression memberInitExpression = (MemberInitExpression)expression;
                            Forms.WinPhoneExpressionSearch.VisitList<Expression>(memberInitExpression.NewExpression.Arguments, new Action<Expression>(this.Visit));
                            Forms.WinPhoneExpressionSearch.VisitList<MemberBinding>(memberInitExpression.Bindings, new Action<MemberBinding>(this.VisitBinding));
                            return;
                        }
                    case ExpressionType.New:
                        {
                            Forms.WinPhoneExpressionSearch.VisitList<Expression>(((NewExpression)expression).Arguments, new Action<Expression>(this.Visit));
                            return;
                        }
                    case ExpressionType.NewArrayInit:
                    case ExpressionType.NewArrayBounds:
                        {
                            Forms.WinPhoneExpressionSearch.VisitList<Expression>(((NewArrayExpression)expression).Expressions, new Action<Expression>(this.Visit));
                            return;
                        }
                    case ExpressionType.Parameter:
                        {
                            throw new ArgumentException(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
                        }
                    case ExpressionType.TypeIs:
                        {
                            this.Visit(((TypeBinaryExpression)expression).Expression);
                            return;
                        }
                    default:
                        {
                            throw new ArgumentException(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
                        }
                }
            }

            private void VisitBinding(MemberBinding binding)
            {
                switch (binding.BindingType)
                {
                    case MemberBindingType.Assignment:
                        {
                            this.Visit(((MemberAssignment)binding).Expression);
                            return;
                        }
                    case MemberBindingType.MemberBinding:
                        {
                            Forms.WinPhoneExpressionSearch.VisitList<MemberBinding>(((MemberMemberBinding)binding).Bindings, new Action<MemberBinding>(this.VisitBinding));
                            return;
                        }
                    case MemberBindingType.ListBinding:
                        {
                            Forms.WinPhoneExpressionSearch.VisitList<ElementInit>(((MemberListBinding)binding).Initializers, (ElementInit initializer) => Forms.WinPhoneExpressionSearch.VisitList<Expression>(initializer.Arguments, new Action<Expression>(this.Visit)));
                            return;
                        }
                }
                throw new ArgumentException(string.Format("Unhandled binding type '{0}'", binding.BindingType));
            }

            private static void VisitList<TList>(IEnumerable<TList> list, Action<TList> visitor)
            {
                foreach (TList tList in list)
                {
                    visitor(tList);
                }
            }

            private void VisitMemberAccess(MemberExpression member)
            {
                if (member.Expression is ConstantExpression && member.Member is FieldInfo)
                {
                    object value = ((ConstantExpression)member.Expression).Value;
                    object obj = ((FieldInfo)member.Member).GetValue(value);
                    if (this.targeType.IsInstanceOfType(obj))
                    {
                        this.results.Add(obj);
                    }
                }
                this.Visit(member.Expression);
            }
        }

    }
}

