using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewsModels
{
    class ViewModelCalculator : ViewModelBase
    {
        int currentState = 1;
        double firstNumber, secondNumber;
        string mathOperator;

        string result;

        public string Result
        {
            get { return result; }
            set
            {
                if(result != value)
                {
                    result = value;
                    OnPropertyChanged();
                }
            }
        }

        string operation;
        public string Operation
        {
            get { return operation; }
            set
            {
                if(operation != value)
                {
                    operation = value;
                    OnPropertyChanged();
                }
            }
        }



        #region Comandos
        public ICommand OnSelectOperator { protected set; get; }
        public ICommand OnClear { protected set; get; }
        public ICommand OnCalculate { protected set; get; }
        public ICommand OnSelectNumber { protected set; get; }

        #endregion

        public ViewModelCalculator()
        {
            OnSelectNumber = new Command<string>(execute: (string parameter) =>
            {
                string pressed = parameter;

                if (Result == "0" || currentState < 0)
                {
                    Result = "";
                    if (currentState < 0)
                        currentState *= -1;
                }

                Result += pressed;

                double number;
                if (double.TryParse(Result, out number))
                {
                    Result = number.ToString("N0");
                    if (currentState == 1)
                    {
                        firstNumber = number;
                    }
                    else
                    {
                        secondNumber = number;
                    }
                }

            });
            
            OnCalculate = new Command<string>(execute: (string parameter) =>
            {
                if (currentState == 2)
                {
                    var result = SimpleCalculator.Calculate(firstNumber, secondNumber, mathOperator);

                    Result = result.ToString();
                    firstNumber = result;
                    currentState = -1;
                }

            });
            OnSelectOperator = new Command<string>(execute: (string parameter) =>
            {
                currentState = -2;
                string pressed = parameter;
                mathOperator = pressed;
            });
            OnClear = new Command(() =>
            {
                firstNumber = 0;
                secondNumber = 0;
                currentState = 1;
                Result = "0";
            });
        }

    }
}
