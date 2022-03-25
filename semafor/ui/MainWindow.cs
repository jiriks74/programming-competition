using System;
using Gtk;
using Cairo;
using System.Threading;

namespace semafor.ui.MainWindow
{
    class MainWindow : Window
    {
        // Value to track playback state
        private bool _play = true;

        // Initialize window builder (interface between the frontend and backend)
        private Builder _builder;

        // Get all the object in the window
        [Builder.Object]
        private DrawingArea pedestRed = null;

        [Builder.Object]
        private DrawingArea pedestGreen = null;

        [Builder.Object]
        private Grid grid = null;


        public static MainWindow Create()
        {
            Builder builder = new Builder(null, "semafor.ui.MainWindow.glade", null);
            return new MainWindow(builder, builder.GetObject("MainWindow").Handle);
        }

        protected MainWindow(Builder builder, IntPtr handle) : base(handle)
        {
            _builder = builder;
            builder.Autoconnect(this);
            SetupHandlers();
            //draw();
        }

        protected void SetupHandlers()
        {
            DeleteEvent += OnLocalDeleteEvent;
        }

        protected void OnLocalDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        private void SetupTimer()
        {
            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.

            // Create a timer that invokes CheckStatus after one second, 
            // and every 1/4 second thereafter.
            // var drawTimer = new Timer(draw(), 100);


            Console.WriteLine("\nDestroying timer.");
        }

        /// <summary>
        /// method that draws all the circles in their respective places
        /// </summary>
        private void draw()
        {
            Cairo.Context cr = Gdk.CairoHelper.Create(GdkWindow);

            cr.LineWidth = 9;
            cr.SetSourceRGB(0.7, 0.2, 0.0);

            int width, height;
            width = Allocation.Width;
            height = Allocation.Height;

            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();

            cr.SetSourceRGB(0.3, 0.4, 0.6);
            cr.Fill();

            ((IDisposable)cr.Target).Dispose();
            ((IDisposable)cr).Dispose();
        }
    }
}
