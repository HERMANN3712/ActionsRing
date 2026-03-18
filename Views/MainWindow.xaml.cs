using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActionsRing.Models;
using ActionsRing.Services;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using Color = System.Windows.Media.Color;
using Cursors = System.Windows.Input.Cursors;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace ActionsRing.Views
{
    public partial class MainWindow : Window
    {
        private readonly List<RingAction> _actions = new();
        private bool _allowRealClose;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            SizeChanged += (_, _) => DrawRing();
            Deactivated += (_, _) =>
            {
                if (IsVisible)
                    Hide();
            };
        }

        public void ShowRing()
        {
            if (!IsVisible)
                Show();

            PositionBottomRight();

            Activate();
            Focus();
            DrawRing();
        }

        public void ForceClose()
        {
            _allowRealClose = true;
            Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _actions.Clear();
            _actions.AddRange(ActionLoader.Load("actions.json"));
            DrawRing();
        }

        private void DrawRing()
        {
            if (RingCanvas == null)
                return;

            RingCanvas.Children.Clear();

            if (_actions.Count == 0 || ActualWidth <= 0 || ActualHeight <= 0)
                return;

            double centerX = ActualWidth / 2;
            double centerY = ActualHeight / 2;

            double radius = 75;
            double iconSize = 48;
            double labelWidth = 64;
            double labelHeight = 16;

            for (int i = 0; i < _actions.Count; i++)
            {
                RingAction action = _actions[i];
                double angle = (2 * Math.PI * i / _actions.Count) - (Math.PI / 2);

                double iconX = centerX + radius * Math.Cos(angle) - iconSize / 2;
                double iconY = centerY + radius * Math.Sin(angle) - iconSize / 2;

                Button button = CreateActionButton(action, iconSize);
                Canvas.SetLeft(button, iconX);
                Canvas.SetTop(button, iconY);
                RingCanvas.Children.Add(button);
                
                double labelX = iconX - 4;
                double labelY = iconY - 4;

                Border label = CreateLabel(action.Title, labelWidth, labelHeight);
                Canvas.SetLeft(label, labelX);
                Canvas.SetTop(label, labelY);
                RingCanvas.Children.Add(label);
            }
        }

        private Button CreateActionButton(RingAction action, double size)
        {
            var button = new Button
            {
                Width = size,
                Height = size,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Cursor = Cursors.Hand,
                ToolTip = action.Title,
                Padding = new Thickness(0),
                Tag = action
            };

            var templateBorder = new Border
            {
                CornerRadius = new CornerRadius(size / 2),
                ClipToBounds = true,
                Width = size,
                Height = size
            };

            if (!string.IsNullOrWhiteSpace(action.Icon) && File.Exists(action.Icon))
            {
                templateBorder.Child = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(Path.GetFullPath(action.Icon))),
                    Stretch = Stretch.UniformToFill
                };
            }
            else
            {
                templateBorder.Child = new TextBlock
                {
                    Text = string.IsNullOrWhiteSpace(action.Title) ? "?" : action.Title[..1].ToUpper(),
                    Foreground = Brushes.White,
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
            }

            button.Content = templateBorder;
            button.Click += ActionButton_Click;

            return button;
        }

        private static Border CreateLabel(string text, double width, double height)
        {
            return new Border
            {
                Width = width,
                Height = height,
                Background = Brushes.White,
                CornerRadius = new CornerRadius(8),
                BorderBrush = new SolidColorBrush(Color.FromRgb(225, 225, 225)),
                BorderThickness = new Thickness(1),
                Child = new TextBlock
                {
                    Text = text,
                    FontSize = 9,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                }
            };
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button { Tag: RingAction action })
                return;

            if (string.IsNullOrWhiteSpace(action.Url))
                return;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = action.Url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible d'ouvrir le lien : {ex.Message}", "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Hide();
        }

        private void CloseButtonBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }

        private void RootGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == sender)
                Hide();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Hide();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!_allowRealClose)
            {
                e.Cancel = true;
                Hide();
                return;
            }

            base.OnClosing(e);
        }

        private void PositionBottomRight()
        {
            if(Screen.PrimaryScreen is not null)
            {
                var workingArea = Screen.PrimaryScreen.WorkingArea;
                Left = workingArea.Right - Width - 10;
                Top = workingArea.Bottom - Height - 10;
            }
        }
    }
}