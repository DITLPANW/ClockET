using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public MainWindow()
        {
            InitializeComponent();
            DrawHoursLines();
            Start();
        }
        public void Start()
        {
            timer.Elapsed += new System.Timers.ElapsedEventHandler(StartClockMove);
            timer.Enabled = true;
        }
        public void StartClockMove(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => 
                {
                    secondHand.Angle = DateTime.Now.Second * 6;
                    minuteHand.Angle = DateTime.Now.Minute * 6;
                    hourHand.Angle = (DateTime.Now.Hour*30) + (DateTime.Now.Minute*0.5);
                    SetDigitalHour(DateTime.Now);
                }));
        }

        public void DrawHoursLines()
        {
            
            Thickness margin = Margin;           
            margin.Left = margin.Right = margin.Top = 0;
            Rectangle [] rectangles = new Rectangle[60];

            for (int i = 0; i < 60; i++)
            {
                rectangles[i] = new Rectangle();
                rectangles[i].Width = 3;
                if (i % 5 == 0)
                {
                    margin.Bottom = 665;
                    rectangles[i].Height = 15;
                    rectangles[i].RenderTransform = new RotateTransform { Angle = i * 6, CenterX = 2, CenterY = 340 };
                }
                else
                {

                    margin.Bottom = 672;
                    rectangles[i].Height = 8;
                    rectangles[i].RenderTransform = new RotateTransform { Angle = i * 6, CenterX = 2, CenterY = 340 };
                }

                rectangles[i].Margin = margin;
                rectangles[i].Fill = Brushes.Black;
                grid.Children.Add(rectangles[i]);
            }

            
        }

        public void SetDigitalHour(DateTime time)
        {
            if(time.Second<10)
            {
                DigitalSecond.Content = "0" + time.Second.ToString();
            }
            else
            {
                DigitalSecond.Content=time.Second.ToString();
            }

            if(time.Minute<10)
            {
                DigitalMinute.Content = "0" + time.Minute.ToString();
            }
            else
            {
                DigitalMinute.Content =time.Minute.ToString();
            }

            if (time.Hour < 10)
            {
                DigitalHour.Content = "0" + time.Hour.ToString();
            }
            else
            {
                DigitalHour.Content =time.Hour.ToString();
            }
        }
    }
}
