using System.Reactive.Concurrency;
using NUnit.Framework;
using ReactiveUI;
using TemperatureConverter.ViewModel;

namespace TemperatureConverter.Tests
{
    [TestFixture]
    public class MainViewModelFixture
    {
        private MainViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new MainViewModel();
            RxApp.MainThreadScheduler = Scheduler.CurrentThread;
        }

        [Test]
        public void GivenWhenInputIsNotDecimalThenErrorIsDisplayed()
        {
            _viewModel.Input = "Input";
            Assert.AreEqual("Please enter a number", _viewModel.ErrorMessage);
            Assert.IsTrue(_viewModel.IsErrorVisible);
        }

        [Test]
        public void GivenWhenInputIsNotDecimalThenErrorIsDisplayedAndWhenInputIsCorrectedErrorMessageDisappears()
        {
            _viewModel.Input = "Input";


            Assert.AreEqual("Please enter a number", _viewModel.ErrorMessage);
            Assert.IsTrue(_viewModel.IsErrorVisible);

            _viewModel.Input = "10";
            Assert.IsTrue(string.IsNullOrEmpty(_viewModel.ErrorMessage));
            Assert.IsFalse(_viewModel.IsErrorVisible);
        }

        [Test]
        public void GivenInputIsCorrectWhenConvertedThenCorrectResultIsDisplayed()
        {
            _viewModel.IsCelciusToFahrenheit = true;
            _viewModel.Input = "45";
            Assert.AreEqual("113", _viewModel.Result);

            _viewModel.Input = "50";
            Assert.AreEqual("122", _viewModel.Result);

        }

        [Test]
        public void GivenInputIsCorrectAndFlagIsFahrenheitToCelciusWhenConvertedThenCorrectResultIsDisplayed()
        {
            _viewModel.IsCelciusToFahrenheit = false;
            _viewModel.Input = "45";
            Assert.AreEqual("7.22", _viewModel.Result);

            _viewModel.Input = "50";
            Assert.AreEqual("10", _viewModel.Result);

        }

        [Test]
        public void GivenInputIsCorrectAndWhenConvertedThenCorrectResultIsDisplayedWhenConversionIsChangedThenCorrectResultAppears()
        {
            _viewModel.IsCelciusToFahrenheit = true;
            _viewModel.Input = "45";
            Assert.AreEqual("113", _viewModel.Result);

            _viewModel.IsCelciusToFahrenheit = false;
            Assert.AreEqual("7.22", _viewModel.Result);
        }
    }
}
