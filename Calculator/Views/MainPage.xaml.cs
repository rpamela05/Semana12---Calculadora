using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Calculator
{    
    public partial class MainPage : ContentPage
    {    
		int currentState = 1;
		string mathOperator;
		double firstNumber, secondNumber;

        public MainPage ()
        {
            InitializeComponent ();
			this.BindingContext = new ViewsModels.ViewModelCalculator();
        }

	}
}
