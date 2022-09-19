namespace MediaPickerIssue;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		var vc = new MainViewController();

		var navigationController = new UINavigationController(rootViewController: vc);
		navigationController.NavigationBar.Translucent = false;

        Window.RootViewController = navigationController;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
}

