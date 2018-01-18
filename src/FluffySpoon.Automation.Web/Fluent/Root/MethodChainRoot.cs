﻿using System;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.DoubleClick;
using FluffySpoon.Automation.Web.Fluent.Drag;
using FluffySpoon.Automation.Web.Fluent.Enter;
using FluffySpoon.Automation.Web.Fluent.Expect;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using FluffySpoon.Automation.Web.Fluent.Find;
using FluffySpoon.Automation.Web.Fluent.Focus;
using FluffySpoon.Automation.Web.Fluent.Hover;
using FluffySpoon.Automation.Web.Fluent.Open;
using FluffySpoon.Automation.Web.Fluent.RightClick;
using FluffySpoon.Automation.Web.Fluent.Select;
using FluffySpoon.Automation.Web.Fluent.TakeScreenshot;
using FluffySpoon.Automation.Web.Fluent.Upload;
using FluffySpoon.Automation.Web.Fluent.Wait;

namespace FluffySpoon.Automation.Web.Fluent.Root
{
	class MethodChainRoot: BaseMethodChainNode, IMethodChainRoot, IBaseMethodChainNode, IAwaitable
	{
		public IExpectMethodChainRoot Expect => MethodChainContext.Enqueue(new ExpectMethodChainRoot());
		public ITakeScreenshotMethodChainNode TakeScreenshot => throw new NotImplementedException();
		public IClickMethodChainNode Click => throw new NotImplementedException();
		public IDoubleClickMethodChainNode DoubleClick => throw new NotImplementedException();
		public IRightClickMethodChainNode RightClick => throw new NotImplementedException();
		public IHoverMethodChainNode Hover => throw new NotImplementedException();
		public IDragMethodChainNode Drag => throw new NotImplementedException();
		public IFocusMethodChainNode Focus => throw new NotImplementedException();
		public ISelectMethodChainNode Select => throw new NotImplementedException();

		public IEnterMethodChainNode Enter(string text) => MethodChainContext.Enqueue(new EnterMethodChainNode(text));

		public IFindMethodChainNode Find(string selector)
		{
			throw new NotImplementedException();
		}

		public IOpenMethodChainNode Open(string uri) => MethodChainContext.Enqueue(new OpenMethodChainNode(uri));
		public IOpenMethodChainNode Open(Uri uri) => Open(uri.ToString());

		public IUploadMethodChainNode Upload(string filePath)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(TimeSpan time)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(int milliseconds)
		{
			throw new NotImplementedException();
		}

		public IWaitMethodChainNode Wait(Func<bool> predicate)
		{
			throw new NotImplementedException();
		}
	}
}
