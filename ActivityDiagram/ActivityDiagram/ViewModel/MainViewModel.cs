using GalaSoft.MvvmLight;
using ActivityDiagram.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace ActivityDiagram.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    /// 

   
    public class MainViewModel : ViewModelBase
    {

        public ICommand AddSquareCommand { get; }
        public ICommand AddCircleCommand { get; }
        public ICommand AddTriangleCommand { get; }

        private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {

            AddSquareCommand = new RelayCommand(AddSquare);
            AddCircleCommand = new RelayCommand(AddCircle);
            AddTriangleCommand = new RelayCommand(AddTriangle);

            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
        }

        private void AddSquare()
        {
           // undoRedoController.AddAndExecute(new AddShapeCommand(Shapes, new Shape()));
        }

        private void AddCircle()
        {
            // undoRedoController.AddAndExecute(new AddShapeCommand(Shapes, new Shape()));
        }

        private void AddTriangle()
        {
            // undoRedoController.AddAndExecute(new AddShapeCommand(Shapes, new Shape()));
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}