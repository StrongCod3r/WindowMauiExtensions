using System.Runtime.InteropServices;
using Microsoft.UI.Windowing;


namespace WindowMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            this.Appearing += MainPage_Appearing;
        }


        private void MainPage_Appearing(object? sender, EventArgs e)
        {
#if WINDOWS
            this.AlwaysOnTop();
            this.ResizeWindow(400, 600);
            this.CentreWindow();
            this.MinimizeToggle(true);
            this.MaximizeToggle(true);
            this.TitleWindow("Main");
            //this.Fullscreen();
#endif
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
