using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace ActivityDiagram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
    public class ViewModelLocator
    {
        internal static void Cleanup()
        {
            throw new NotImplementedException();
        }
    }

}
