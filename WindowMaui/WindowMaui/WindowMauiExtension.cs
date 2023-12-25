using System.Runtime.CompilerServices;
using Microsoft.UI;
using Microsoft.UI.Windowing;

namespace WindowMaui
{
    public static class WindowMauiExtension
    {
#if WINDOWS
        private static MauiWinUIWindow GetMauiWinUIWindow(this ContentPage contentPage)
        {
            try
            {
                var window = contentPage.GetParentWindow().Handler.PlatformView as MauiWinUIWindow;
                return window;
            }
            catch (Exception)
            {
                throw new Exception("Window is not created");
            }
        }

        private static AppWindow GetAppWindow(MauiWinUIWindow window)
        {
            try
            {
                var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                var id = Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = AppWindow.GetFromWindowId(id);
                return appWindow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Window GetWindow(this ContentPage contentPage)
        {
            try
            {
                return contentPage.GetParentWindow();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void MaximizeToggle(this ContentPage contentPage, bool? enable = null)
        {
            var windowMaui = GetMauiWinUIWindow(contentPage);
            var appWindow = GetAppWindow(windowMaui);
            var overlappedPresenter = (OverlappedPresenter)appWindow.Presenter;

            if (enable.HasValue)
            {
                overlappedPresenter.IsMaximizable = enable.Value;
            }
            else
            {
                overlappedPresenter.IsMaximizable = !overlappedPresenter.IsMaximizable;
            }
        }

        public static void MinimizeToggle(this ContentPage contentPage, bool? enable = null)
        {
            var windowMaui = GetMauiWinUIWindow(contentPage);
            var appWindow = GetAppWindow(windowMaui);
            var overlappedPresenter = (OverlappedPresenter)appWindow.Presenter;

            if (enable.HasValue)
            {
                overlappedPresenter.IsMinimizable = enable.Value;
            }
            else
            {
                overlappedPresenter.IsMinimizable = !overlappedPresenter.IsMinimizable;
            }
        }

        public static void TitleWindow(this ContentPage contentPage, string title)
        {
            var window = GetWindow(contentPage);
            window.Title = title;
        }

        public static void ResizeWindow(this ContentPage contentPage, double width, double height)
        {
            var window = GetWindow(contentPage);
            window.Width = width;
            window.Height = height;
        }

        public static void AlwaysOnTop(this ContentPage contentPage)
        {
            var windowMaui = GetMauiWinUIWindow(contentPage);
            var appWindow = GetAppWindow(windowMaui);
            var presenter = appWindow.Presenter as OverlappedPresenter;

            //appWindow.SetPresenter(AppWindowPresenterKind.CompactOverlay);
            presenter.IsAlwaysOnTop = true;
        }

        public static void CentreWindow(this ContentPage contentPage)
        {
            var window = GetWindow(contentPage);

            double screenWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
            double screenHeight = DeviceDisplay.Current.MainDisplayInfo.Height;

            window.X = (screenWidth / 2) - (window.Width / 2);
            window.Y = (screenHeight / 2) - (window.Height / 2);
        }

        public static void FullscreenToggle(this ContentPage contentPage, bool? fullscreen = null)
        {
            var windowMaui = GetMauiWinUIWindow(contentPage);
            var appWindow = GetAppWindow(windowMaui);
            var overlappedPresenter = (OverlappedPresenter)appWindow.Presenter;
          
            if (fullscreen.HasValue)
            {
                if (fullscreen.Value)
                {
                    if (overlappedPresenter.State != OverlappedPresenterState.Maximized)
                    {
                        overlappedPresenter.SetBorderAndTitleBar(false, false);
                        overlappedPresenter.Maximize();
                    }
                }
                else
                {
                    overlappedPresenter.SetBorderAndTitleBar(true, true);
                    overlappedPresenter.Restore();
                }  
            }
            else
            {
                if (overlappedPresenter.State == OverlappedPresenterState.Maximized)
                {
                    overlappedPresenter.SetBorderAndTitleBar(true, true);
                    overlappedPresenter.Restore();
                }
                else
                {
                    overlappedPresenter.SetBorderAndTitleBar(false, false);
                    overlappedPresenter.Maximize();
                }
            }
        }
#endif
    }
}
