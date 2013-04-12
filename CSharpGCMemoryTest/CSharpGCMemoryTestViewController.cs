using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CSharpGCMemoryTest
{
	public partial class CSharpGCMemoryTestViewController : UIViewController
	{
		private const UInt32 kSmallMemoryBlockSize =  1;
		private const UInt32 kMediumMemoryBlockSize = 1024;
		private const UInt32 kLargeMemoryBlockSize =  10 * 1024 * 1024;
		
		private const UInt32 kSmallBlockLoopSize =  50000;
		private const UInt32 kMediumBlockLoopSize = 5000;
		private const UInt32 kLargeBlockLoopSize =  500;

		private NSOperationQueue testOperationQueue;
		private NSDate startTime;
		private NSData dataProp;
		private NSArray arrayProp;
		private NSSet setProp;
		private MemoryStream memoryStream;
		private ArrayList arrayList;
		private Dictionary<string, string> dictionary;

		public CSharpGCMemoryTestViewController () : base ("CSharpGCMemoryTestViewController", null)
		{
			testOperationQueue = new NSOperationQueue();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		private void performMemoryTest(UInt32 loopSize, UInt32 blockSize, bool monoObjects)
		{
			NSAction testAction = () => {
				this.startTime = new NSDate();
				if (monoObjects) {
					this.allocateMonoMemoryWithLoopSize(loopSize, blockSize);
				} else {
					this.allocateMemoryWithLoopSize(loopSize, blockSize);
				}
				this.InvokeOnMainThread(() => {
					this.displayTimerResults();
				});
			};
			NSOperation testOperation = NSBlockOperation.Create(testAction);
			UIApplication.SharedApplication.IdleTimerDisabled = true;
			this.testOperationQueue.AddOperation(testOperation);
		}

		partial void smallFoundationTapped(NSObject sender)
		{
			this.performMemoryTest(kSmallBlockLoopSize, kSmallMemoryBlockSize, false);
		}
		
		partial void mediumFoundationTapped(NSObject sender)
		{
			this.performMemoryTest(kMediumBlockLoopSize, kMediumMemoryBlockSize, false);
		}

		partial void largeFoundationTapped(NSObject sender)
		{
			this.performMemoryTest(kLargeBlockLoopSize, kLargeMemoryBlockSize, false);
		}
		
		partial void smallMonoTapped(NSObject sender)
		{
			this.performMemoryTest(kSmallBlockLoopSize, kSmallMemoryBlockSize, true);
		}
		
		partial void mediumMonoTapped(NSObject sender)
		{
			this.performMemoryTest(kMediumBlockLoopSize, kMediumMemoryBlockSize, true);
		}
		
		partial void largeMonoTapped(NSObject sender)
		{
			this.performMemoryTest(kLargeBlockLoopSize, kLargeMemoryBlockSize, true);
		}

		private void allocateMemoryWithLoopSize(UInt32 loopSize, UInt32 blockSize)
		{
			for (UInt32 i = 0; i < loopSize; i++) {
				NSData data = new NSMutableData(blockSize);
				NSArray array = new NSMutableArray((Int32)blockSize);
				NSSet set = new NSMutableSet((Int32)blockSize);
				this.dataProp = data;
				this.arrayProp = array;
				this.setProp = set;
			}
		}

		private void allocateMonoMemoryWithLoopSize(UInt32 loopSize, UInt32 blockSize)
		{
			for (UInt32 i = 0; i < loopSize; i++) {
				MemoryStream data = new MemoryStream((int)blockSize);
				ArrayList array = new ArrayList((int)blockSize);
				Dictionary<string, string> dictionary = new Dictionary<string, string>((int)blockSize);
				this.memoryStream = data;
				this.arrayList = array;
				this.dictionary = dictionary;
			}
		}

		private void displayTimerResults()
		{
			UIApplication.SharedApplication.IdleTimerDisabled = false;
			NSDate endTime = new NSDate();
			double interval = endTime.SecondsSinceReferenceDate - startTime.SecondsSinceReferenceDate;
			NSNumber timeNumber = new NSNumber(interval);
			NSNumberFormatter formatter = new NSNumberFormatter();
			formatter.MaximumFractionDigits = 5;
			string formattedTime = formatter.StringFromNumber(timeNumber);
			this.timeLabel.Text = formattedTime;
		}
	}
}
