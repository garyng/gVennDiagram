using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gVennDiagram
{
	class VennDiagramHelper
	{
		#region Diagram
		private static int _iRadius = 25;
		private List<Point> _plOneCircle = new List<Point>() { new Point(50, 50) };
		private List<Point> _plTwoCircle = new List<Point>() { new Point(33, 50), new Point(66, 50) };
		private List<Point> _plThreeCircle = new List<Point>() { new Point(48, 33), new Point(33, 60), new Point(66, 66) };
		#endregion

		#region Operators
		private string[] _strOperators = { "n", "u", "'", "(", ")" };
		private string[] _strLongOperators = { "intersect", "union" };
		private int _iCircleLimit = 3;
		#endregion

		#region Private Field
		private string _strEquation;
		private int _iCircleCount;
		#endregion

		#region Execption
		static class gVennDiagramException
		{
			public static Exception OutOfCircleLimit = new Exception("The number of circle used is more than limit.");

			public static Exception NoCircleIsUsed = new Exception("No circle is used.");
		}
		#endregion

		public VennDiagramHelper(string Equation)
		{
			_strEquation = Equation;
		}

		public void Operate()
		{
			FormatEquation(ref _strEquation);
			_iCircleCount = CalculateCircleCount(_strEquation);
			CheckCircleCount(_iCircleCount);
		}

		#region FormatEquation
		private void FormatEquation(ref string equation)
		{
			equation = ReplaceWhiteSpace(equation);
			equation = ReplaceLongOperators(equation);
		}

		private string ReplaceWhiteSpace(string equation)
		{
			return Regex.Replace(equation, " ", "");
		}

		private string ReplaceLongOperators(string equation)
		{
			string equ = equation;
			for (int i = 0; i < _strLongOperators.Count(); i++)
			{
				equ = Regex.Replace(equ, _strLongOperators[i], _strOperators[i]);
			}
			return equ;
		}
		#endregion

		private int CalculateCircleCount(string equation)
		{
			string equ = equation;
			_strOperators.ToList().ForEach(item => equ = equ.Replace(item, ""));
			return equ.Distinct().ToList().Count;
		}

		#region CheckCircleCount
		private void CheckCircleCount(string equation)
		{
			CheckCircleCount(CalculateCircleCount(equation));
		}

		private void CheckCircleCount(int count)
		{
			if (count > _iCircleLimit)
			{
				throw gVennDiagramException.OutOfCircleLimit;
			}
			else if (count <= 0)
			{
				throw gVennDiagramException.NoCircleIsUsed;
			}
		}
		#endregion
	}
}
