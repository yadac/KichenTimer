using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UIKit;

namespace KichenTimer
{
    public partial class MyViewController : UIViewController
    {
        public MyViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            var buttons = new List<UIButton>()
            {
                StartButton,
                ClearButton,
                Add10MinButton,
                Add1MinButton,
                Add10SecButton,
                Add1SecButton,
            };

            foreach (var button in buttons)
            {
                SetButtonOutline(button);
            }

            // ClickEventHandler
            Add10MinButton.TouchUpInside += Add10MinButtonTouchUpInside;
            Add1MinButton.TouchUpInside += Add1MinButtonTouchUpInside;
            Add10SecButton.TouchUpInside += Add10SecButtonTouchUpInside;
            Add1SecButton.TouchUpInside += Add1SecButtonTouchUpInside;
            StartButton.TouchUpInside += StartButtonTouchUpInside;

            // countdownTimer
            _timer = new Timer(TimerOnTick, null, 0, 100);
        }

        private void TimerOnTick(object state)
        {
            if (!_start) return;

            // Do in UI Thread
            InvokeOnMainThread(() =>
            {
                _remain = _remain.Add(TimeSpan.FromMilliseconds(-100));
                ShowRemain();

                // if not remain (= zero seconds), change button status and alert
                if (!(_remain.TotalSeconds <= 0)) return;
                _start = !_start;
                _remain = new TimeSpan(0);
                StartButton.SetTitle("Start", UIControlState.Normal);

                // alert
            });

        }

        private bool _start;
        private Timer _timer;

        private void StartButtonTouchUpInside(object sender, EventArgs e)
        {
            _start = !_start;
            StartButton.SetTitle(_start ? "Stop" : "Start", UIControlState.Normal);
        }

        private void SetButtonOutline(UIButton btn)
        {
            btn.Layer.CornerRadius = 3;
            btn.Layer.BorderColor = UIColor.LightGray.CGColor; // what's CGColor?
            btn.Layer.BorderWidth = 1;
        }

        private void Add10MinButtonTouchUpInside(object sender, EventArgs e)
        {
            _remain = _remain.Add(TimeSpan.FromMinutes(10));
            ShowRemain();
        }

        private void Add1MinButtonTouchUpInside(object sender, EventArgs e)
        {
            _remain = _remain.Add(TimeSpan.FromMinutes(1));
            ShowRemain();
        }

        private void Add10SecButtonTouchUpInside(object sender, EventArgs e)
        {
            _remain = _remain.Add(TimeSpan.FromSeconds(10));
            ShowRemain();
        }

        private void Add1SecButtonTouchUpInside(object sender, EventArgs e)
        {
            _remain = _remain.Add(TimeSpan.FromSeconds(1));
            ShowRemain();
        }

        private void ShowRemain()
        {
            Debug.WriteLine($"{_remain.Minutes}:{_remain.Seconds}");
            RemainLabel.Text = $"{_remain.Minutes:f0}:{_remain.Seconds:d2}";
        }

        private TimeSpan _remain = new TimeSpan(0);
    }
}