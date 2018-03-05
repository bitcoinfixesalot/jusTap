using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Services;
using TapFast2.ViewModel;

namespace TapFast2
{
    public class ViewModelLocator
    {
        private UnityContainer _unityContainer;

        public OptionsViewModel OptionsViewModel
        {
            get
            {
                return _unityContainer.Resolve<OptionsViewModel>();
            }
        }

        public GameOverViewModel GameOverViewModel
        {
            get
            {
                return _unityContainer.Resolve<GameOverViewModel>();
            }
        }

        public LeaderboardViewModel NormalLeaderboardViewModel
        {
            get
            {
                return _unityContainer.Resolve<LeaderboardViewModel>(Constants.ViewModels.NORMAL_LEADERBOARD);
            }
        }

        public LeaderboardViewModel ArcadeLeaderboardViewModel
        {
            get
            {
                return _unityContainer.Resolve<LeaderboardViewModel>(Constants.ViewModels.ARCADE_LEADERBOARD);
            }
        }

        public AboutViewModel AboutViewModel
        {
            get
            {
                return _unityContainer.Resolve<AboutViewModel>();
            }
        }

        public HowToViewModel HowToViewModel
        {
            get
            {
                return _unityContainer.Resolve<HowToViewModel>();
            }
        }

        public INavigationService NavigationService
        {
            get
            {
                return _unityContainer.Resolve<INavigationService>();
            }
        }

        public ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IInAppPurchaseService, InAppPurchaseService>(new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<OptionsViewModel>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<GameOverViewModel>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<LeaderboardViewModel>(Constants.ViewModels.NORMAL_LEADERBOARD, new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<LeaderboardViewModel>(Constants.ViewModels.ARCADE_LEADERBOARD, new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<AboutViewModel>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<HowToViewModel>(new ContainerControlledLifetimeManager());

            var unityServiceLocator = new UnityServiceLocator(_unityContainer);

            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }
    }
}
