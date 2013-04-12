// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace CSharpGCMemoryTest
{
	[Register ("CSharpGCMemoryTestViewController")]
	partial class CSharpGCMemoryTestViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel memWarningLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel timeLabel { get; set; }

		[Action ("smallFoundationTapped:")]
		partial void smallFoundationTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("mediumFoundationTapped:")]
		partial void mediumFoundationTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("largeFoundationTapped:")]
		partial void largeFoundationTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("smallMonoTapped:")]
		partial void smallMonoTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("mediumMonoTapped:")]
		partial void mediumMonoTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("largeMonoTapped:")]
		partial void largeMonoTapped (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (memWarningLabel != null) {
				memWarningLabel.Dispose ();
				memWarningLabel = null;
			}

			if (timeLabel != null) {
				timeLabel.Dispose ();
				timeLabel = null;
			}
		}
	}
}
