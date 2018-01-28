﻿using System;
using System.Threading.Tasks;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Click;
using FluffySpoon.Automation.Web.Fluent.Context;
using FluffySpoon.Automation.Web.Fluent.DoubleClick;
using FluffySpoon.Automation.Web.Fluent.Drag;
using FluffySpoon.Automation.Web.Fluent.Enter;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using FluffySpoon.Automation.Web.Fluent.Find;
using FluffySpoon.Automation.Web.Fluent.Focus;
using FluffySpoon.Automation.Web.Fluent.Hover;
using FluffySpoon.Automation.Web.Fluent.Open;
using FluffySpoon.Automation.Web.Fluent.RightClick;
using FluffySpoon.Automation.Web.Fluent.Select;
using FluffySpoon.Automation.Web.Fluent.TakeScreenshot;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using FluffySpoon.Automation.Web.Fluent.Targets.To;
using FluffySpoon.Automation.Web.Fluent.Upload;
using FluffySpoon.Automation.Web.Fluent.Wait;

namespace FluffySpoon.Automation.Web.Fluent.Root
{
	class MethodChainRoot : MethodChainRoot<IBaseMethodChainNode>
	{

	}

	class MethodChainRoot<TParentMethodChainNode>: 
		BaseMethodChainNode<TParentMethodChainNode>, 
		IMethodChainRoot, 
		IBaseMethodChainNode, 
		IAwaitable
		where TParentMethodChainNode : IBaseMethodChainNode
	{
		public IExpectMethodChainRoot Expect => 
			MethodChainContext.Enqueue(new ExpectMethodChainRoot<IBaseMethodChainNode>());

		public IDomElementOfTargetMethodChainNode<IBaseMethodChainNode, ITakeScreenshotOfTargetMethodChainNode> TakeScreenshot =>
			MethodChainContext.Enqueue(new TakeScreenshotChainNode());

		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IClickOnTargetsMethodChainNode> Click => 
			MethodChainContext.Enqueue(new ClickMethodChainNode());
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IDoubleClickOnTargetsMethodChainNode> DoubleClick =>
			MethodChainContext.Enqueue(new DoubleClickMethodChainNode());
		public IMouseOnTargetsMethodChainNode<IBaseMethodChainNode, IRightClickOnTargetsMethodChainNode> RightClick =>
			MethodChainContext.Enqueue(new RightClickMethodChainNode());

		public IMouseOnTargetMethodChainNode<IBaseMethodChainNode, IHoverOnTargetMethodChainNode> Hover =>
			MethodChainContext.Enqueue(new HoverMethodChainNode());

		public IMouseFromTargetMethodChainNode<IBaseMethodChainNode, IMouseToTargetMethodChainNode<IBaseMethodChainNode, IDragFromTargetToTargetMethodChainNode>> Drag =>
			MethodChainContext.Enqueue(new DragMethodChainNode());

		public IMouseOnTargetMethodChainNode<IBaseMethodChainNode, IFocusOnTargetMethodChainNode> Focus =>
			MethodChainContext.Enqueue(new FocusMethodChainNode());

		public ISelectMethodChainNode Select =>
			MethodChainContext.Enqueue(new SelectMethodChainNode());

		public IDomElementInTargetsMethodChainNode<IBaseMethodChainNode, IEnterInTargetMethodChainNode> Enter(string text) =>
			MethodChainContext.Enqueue(new EnterMethodChainNode(text));

		public IOpenMethodChainNode Open(string uri) => 
			MethodChainContext.Enqueue(new OpenMethodChainNode(uri));
		public IOpenMethodChainNode Open(Uri uri) => 
			Open(uri.ToString());

		public IUploadMethodChainNode Upload(string filePath)
		{
			throw new NotImplementedException();
		}

		public IMethodChainRoot Wait(TimeSpan timespan)
		{
			DateTime? startTime = null;
			return Wait(() =>
			{
				if (startTime == null)
					startTime = DateTime.UtcNow;

				return DateTime.UtcNow - startTime > timespan;
			});
		}
		public IMethodChainRoot Wait(int milliseconds) => Wait(TimeSpan.FromMilliseconds(milliseconds));
		public IMethodChainRoot Wait(Func<Task<bool>> predicate) => MethodChainContext.Enqueue(new WaitMethodChainNode(predicate));
		public IMethodChainRoot Wait(Func<bool> predicate)
		{
			return Wait(() =>
			{
				return Task.FromResult(predicate());
			});
		}
		public IMethodChainRoot Wait(Action<IExpectMethodChainRoot> predicate)
		{
			var methodChainContext = new MethodChainContext(MethodChainContext.Frameworks);

			var methodChainRoot = new MethodChainRoot();
			methodChainRoot.MethodChainContext = methodChainContext;

			return Wait(async () =>
			{
				predicate(methodChainRoot.Expect);

				var expectationFailed = true;
				while (expectationFailed)
				{
					try
					{
						await methodChainRoot;
						expectationFailed = false;
					}
					catch (ExpectationNotMetException)
					{
						methodChainContext.ResetLastError();
						expectationFailed = true;

						await Task.Delay(100);
					}
				}

				return true;
			});
		}
	}
}
