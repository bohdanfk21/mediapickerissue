using System;
using Microsoft.Maui.Media;

namespace MediaPickerIssue
{
    public class MainViewController : UIViewController
    {
        #region overrides
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if(_button != null)
                _button.TouchUpInside += Button_TouchUpInside;
            if(_buttonWithDelay != null)
                _buttonWithDelay.TouchUpInside += ButtonWithDelay_TouchUpInside;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if(_button != null)
                _button.TouchUpInside -= Button_TouchUpInside;

            if (_buttonWithDelay != null)
                _buttonWithDelay.TouchUpInside -= ButtonWithDelay_TouchUpInside;
        }
        #endregion

        #region private methods
        private void InitView()
        {
            View!.BackgroundColor = UIColor.White;
            _button = new UIButton()
            {
                BackgroundColor = UIColor.Gray,
            };
            _button.SetTitleColor(UIColor.White, UIControlState.Normal);
            _button.SetTitle("Pick photo", UIControlState.Normal);
            View!.AddSubview(_button);
            _button.TranslatesAutoresizingMaskIntoConstraints = false;
            _button.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;
            _button.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            _buttonWithDelay = new UIButton()
            {
                BackgroundColor = UIColor.Gray,
            };
            _buttonWithDelay.SetTitleColor(UIColor.White, UIControlState.Normal);
            _buttonWithDelay.SetTitle("Pick photo with delay", UIControlState.Normal);
            View!.AddSubview(_buttonWithDelay);
            _buttonWithDelay.TranslatesAutoresizingMaskIntoConstraints = false;
            _buttonWithDelay.TopAnchor.ConstraintEqualTo(_button.BottomAnchor, 32).Active = true;
            _buttonWithDelay.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

        }

        private async void Button_TouchUpInside(object? sender, EventArgs e)
        {
            var photo = await MediaPicker.PickPhotoAsync();
            if(photo != null)
            {
                await OpenModalWindow();
            }
        }

        private async Task OpenModalWindow()
        {
            var viewController = new ModalViewController();
            viewController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
            await PresentViewControllerAsync(viewController, true);
        }

        private async void ButtonWithDelay_TouchUpInside(object? sender, EventArgs e)
        {
            var photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
            {
                await Task.Delay(1000);
                await OpenModalWindow();
            }
        }
        #endregion

        #region private properties
        private UIButton? _button;
        private UIButton? _buttonWithDelay;
        #endregion
    }

    public class ModalViewController : UIViewController
    {
        #region overrides
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitView();
        }
        #endregion

        #region private methods
        private void InitView()
        {
            View!.BackgroundColor = UIColor.Purple;
            var label = new UILabel()
            {
                BackgroundColor = UIColor.Gray,
                TextColor = UIColor.White,
                Text = "Modal window"
            };
            View!.AddSubview(label);
            label.TranslatesAutoresizingMaskIntoConstraints = false;
            label.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;
            label.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
        }
        #endregion
    }
}

