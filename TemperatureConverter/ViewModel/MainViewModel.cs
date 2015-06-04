using System;
using System.Reactive.Subjects;
using ReactiveUI;

namespace TemperatureConverter.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private string _input;
        private string _errorMessage;
        private bool _isErrorVisible;
        private string _result;
        private bool _isCelciusToFahrenheit;

        private readonly Subject<string> _inputSubject = new Subject<string>();

        public string Input
        {
            get { return _input; }
            set { _input = value; _inputSubject.OnNext(value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; this.RaisePropertyChanged(); }
        }

        public bool IsErrorVisible
        {
            get { return _isErrorVisible; }
            set { _isErrorVisible = value; this.RaisePropertyChanged(); }
        }

        public ReactiveCommand<object> ConvertCommand { get; set; }

        public string Result
        {
            get { return _result; }
            set { _result = value; this.RaisePropertyChanged(); }
        }

        public bool IsCelciusToFahrenheit
        {
            get { return _isCelciusToFahrenheit; }
            set { _isCelciusToFahrenheit = value; _inputSubject.OnNext(Input); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _inputSubject.Subscribe(ActionInputSubject);
            IsCelciusToFahrenheit = true;
            // No Need for user to click on button as results can be shown immediately
            //ConvertCommand = ReactiveCommand.Create(this.ObservableForProperty(x => x.Input).Select(x => !string.IsNullOrEmpty(x.Value)).StartWith(false));
            //ConvertCommand.Subscribe(ExecuteConvertCommand);
        }

        private void ActionInputSubject(string d)
        {
            if (d == null)
                return;

            decimal outValue;
            if (!Decimal.TryParse(d, out outValue))
            {
                IsErrorVisible = true;
                ErrorMessage = "Please enter a number";
                Result = string.Empty;
                return;
            }

            IsErrorVisible = false;
            ErrorMessage = string.Empty;

            Result = Convert.ToString(Math.Round(IsCelciusToFahrenheit ? (outValue * 9 / 5 + 32) : (outValue - 32) * 5 / 9, 2));
        }
    }
}