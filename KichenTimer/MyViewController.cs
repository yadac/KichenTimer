using Foundation;
using System;
using System.Collections.Generic;
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

            Add10MinButton.TouchUpInside += Add10MinButtonTouchUpInside;

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

        private void ShowRemain()
        {
            RemainLabel.Text = $"{_remain.TotalMinutes:f0}:{_remain.Seconds:d2}";
        }

        private TimeSpan _remain = new TimeSpan(0);
    }
}