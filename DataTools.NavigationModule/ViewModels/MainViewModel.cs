using System;
using System.Windows.Input;
using Prism.Commands;

namespace DataTools.NavigationModule.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICommand _nextCommand;
        private readonly ICommand _closeCommand;
        private readonly ICommand _previousCommand;

        public override ICommand NextCommand => _nextCommand;

        public override ICommand CloseCommand => _closeCommand;

        public override ICommand PreviousCommand => _previousCommand;

        public MainViewModel()
        {
            _nextCommand = new DelegateCommand(OnNavigateNext);
            _closeCommand = new DelegateCommand(OnClose);
            _previousCommand = new DelegateCommand(OnPrevious);
        }

        private void OnNavigateNext()
        {
            RequestNavigate(new Uri("ViewOne", UriKind.Relative), NavigationCallback);
        }

        private void OnClose()
        {
        }

        private void OnPrevious()
        {
            NavigationJournal.GoBack();
        }
    }
}