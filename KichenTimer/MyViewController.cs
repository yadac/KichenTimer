using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // set eventhandler
            Add10MinButton.TouchUpInside += Add10MinButtonTouchUpInside;
            Add1MinButton.TouchUpInside += Add1MinButtonTouchUpInside;
            Add10SecButton.TouchUpInside += Add10SecButtonTouchUpInside;
            Add1SecButton.TouchUpInside += Add1SecButtonTouchUpInside;

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