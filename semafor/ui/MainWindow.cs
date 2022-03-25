using System;
using Gtk;
using Cairo;
using System.Timers;

namespace semafor.ui.MainWindow
{
    class MainWindow : Window
    {
        // Value to track playback state
        private bool _play = true;

        // Values for switching timings - how long will each be turn on for
        private int carRedTime = 5;
        private int carOrangeTime = 2;
        private int carGreenTime = 10;
        private int elapsedSeconds = 0; // Seconds elapsed from last switch

        // Value to track lights for car
        private bool carRedLight = true;
        private bool carGreenLight = false;

        // Initialize window builder (interface between the frontend and backend)
        private Builder _builder;

        // Get all the object in the window
        [Builder.Object]
        private DrawingArea pedestRed;

        [Builder.Object]
        private DrawingArea pedestGreen;

        [Builder.Object]
        private DrawingArea carRed;
        [Builder.Object]
        private DrawingArea carOrange;
        [Builder.Object]
        private DrawingArea carGreen;


        [Builder.Object]
        private Button playButton = null;
        [Builder.Object]
        private Window mainWindow;


        public static MainWindow Create()
        {
            Builder builder = new Builder(null, "semafor.ui.MainWindow.glade", null);
            return new MainWindow(builder, builder.GetObject("mainWindow").Handle);
        }

        protected MainWindow(Builder builder, IntPtr handle) : base(handle)
        {
            _builder = builder;
            builder.Autoconnect(this);
            SetupHandlers();
            InitTimer();
        }

        protected void SetupHandlers()
        {
            DeleteEvent += OnLocalDeleteEvent;
            // playButton.Activate += playButtonClick();
        }

        protected void OnLocalDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        private void InitLights(object source, ElapsedEventArgs e){
            drawRed(carRed);
            drawGreen(pedestGreen);

            renderColors.Enabled = false;
            timer.Enabled = true;
        }

        private Timer timer;
        private Timer renderColors;
        private void InitTimer()
        {
            // Create a timer
            timer = new Timer();
            // Tell the timer what to do when it elapses
            timer.Elapsed += new ElapsedEventHandler(updateTime);
            // Set it to go off every five seconds
            timer.Interval = 1000;
            // And stop it
            timer.Enabled = false;

            renderColors = new Timer();
            renderColors.Interval = 1500;
            renderColors.Elapsed += new ElapsedEventHandler(InitLights);
            renderColors.Enabled = true;
        }

        private void updateTime(object source, ElapsedEventArgs e)
        {
            if(carRedLight && elapsedSeconds >= carRedTime){
                carRedLight = false;
                carGreenLight = false;
                elapsedSeconds = 0;
                draw();
                return;
            }
            else if (!carRedLight && !carGreenLight && elapsedSeconds >= carOrangeTime){
                carRedLight = false;
                carGreenLight = true;
                elapsedSeconds = 0;
                draw();
                return;
            }
            else if(carGreenLight && elapsedSeconds >= carGreenTime){
                carRedLight = true;
                carGreenLight = false;
                elapsedSeconds = 0;
                draw();
                return;
            }
            else{
                elapsedSeconds += 1;
                return;
            }
        }

        /// <summary>
        /// method that draws all the circles in their respective places
        /// </summary>
        private void draw()
        {
            if (carRedLight)
            {
                //carRed;
                drawRed(carRed);
                drawBlank(carOrange);
                drawBlank(carGreen);

                drawBlank(pedestRed);
                //pedestGreen;
                drawGreen(pedestGreen);
                return;
            }
            else
            {
                //pedestRed;
                drawRed(pedestRed);
                drawBlank(pedestGreen);

                if (carGreenLight)
                {
                    //carGreen;
                    drawGreen(carGreen);

                    drawBlank(carRed);
                    drawBlank(carOrange);
                    return;
                }
                else
                {
                    //carRed;
                    drawRed(carRed);
                    //carOrange;
                    drawOrange(carOrange);

                    drawBlank(carGreen);
                    return;
                }
            }
        }

        private void drawRed(DrawingArea target)
        {
            int width, height;
            Cairo.Context cr = Gdk.CairoHelper.Create(target.GdkWindow);

            cr.LineWidth = 9;
            cr.SetSourceRGB(1, 0, 0);

            width = carRed.AllocatedWidth;
            height = carRed.AllocatedHeight;

            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();

            cr.SetSourceRGB(1, 0, 0);
            cr.Fill();

            ((IDisposable)cr.Target).Dispose();
            ((IDisposable)cr).Dispose();
        }

        private  void drawGreen(DrawingArea target)
        {
            int width, height;
            Cairo.Context cr = Gdk.CairoHelper.Create(target.GdkWindow);

            cr.LineWidth = 9;
            cr.SetSourceRGB(0, 1, 0);

            width = carRed.AllocatedWidth;
            height = carRed.AllocatedHeight;

            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();

            cr.SetSourceRGB(0, 1, 0);
            cr.Fill();

            ((IDisposable)cr.Target).Dispose();
            ((IDisposable)cr).Dispose();
        }

        private  void drawOrange(Gtk.DrawingArea target)
        {
            int width, height;
            Cairo.Context cr = Gdk.CairoHelper.Create(target.GdkWindow);

            cr.LineWidth = 9;
            cr.SetSourceRGB(1, 0.75, 0);

            width = carRed.AllocatedWidth;
            height = carRed.AllocatedHeight;

            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();

            cr.SetSourceRGB(1, 0.75, 0);
            cr.Fill();

            ((IDisposable)cr.Target).Dispose();
            ((IDisposable)cr).Dispose();
        }
        private  void drawBlank(DrawingArea target)
        {
            int width, height;
            Cairo.Context cr = Gdk.CairoHelper.Create(target.GdkWindow);

            cr.LineWidth = 9;
            cr.SetSourceRGB(0, 0, 0);

            width = carRed.AllocatedWidth;
            height = carRed.AllocatedHeight;

            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();

            cr.SetSourceRGB(0, 0, 0);
            cr.Fill();

            ((IDisposable)cr.Target).Dispose();
            ((IDisposable)cr).Dispose();
        }
    }
}
