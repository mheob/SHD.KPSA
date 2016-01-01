using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Regions;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace DataTools.NavigationModule
{
    public class ViewModelBase : INavigateAsync
    {
        private readonly IRegionManager _regionManager;
#pragma warning disable 169
        private IUnityContainer _container;
#pragma warning restore 169
        protected IRegion MainRegion;

        /// <summary>
        /// Das NavigationsJournal beinhaltet alle relevanten Uri's
        /// die in der Navigation betroffen sind und waren.
        /// </summary>
        public virtual IRegionNavigationJournal NavigationJournal { get; private set; }

        /// <summary>
        /// Command für das Anzeigen der nächsten Seite.
        /// </summary>
        public virtual ICommand NextCommand { get; private set; }

        /// <summary>
        /// Command für das Schließen der Applikation.
        /// </summary>
        public virtual ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Command für das Navigieren zur vorherigen Seite.
        /// </summary>
        public virtual ICommand PreviousCommand { get; private set; }

        /// <summary>
        /// Konstruktor lokalisiert mit dem ServiceLocator bereits instantiierte Klassen
        /// die mit dem UnityContainer bereits registriert worden sind.
        /// </summary>
        public ViewModelBase()
        {
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            IRegionNavigationService navigationService = _regionManager.Regions["MainRegion"].NavigationService;

            NavigationJournal = navigationService.Journal;
            MainRegion = _regionManager.Regions["MainRegion"];
        }

        public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback)
        {
            _regionManager.RequestNavigate(MainRegion.Name, target, navigationCallback);
        }

        public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback,
            NavigationParameters navigationParameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notwendige Methode um auf .RequestNavigate reagieren zu können.
        /// </summary>
        /// <param name="res"></param>
        public void NavigationCallback(NavigationResult res)
        {
            ClearUnusedViewsFromReagion(MainRegion, res.Context.Uri.OriginalString);
        }

        /// <summary>
        /// Diese Methode übernimmt, das Entfernen von Views, die nicht mehr angezeigt
        /// werden sollen. Da die Views nicht mit .Add zur Region hinzugefügt wurden,
        /// werden hier die Namen mit dem ViewModel verglichen.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="viewName"></param>
        protected void ClearUnusedViewsFromReagion(IRegion region, string viewName)
        {
            foreach (var view in region.Views.ToList())
            {
                if (!view.GetType().Name.Contains(viewName))
                {
                    region.Remove(view);
                }
            }
        }
    }
}