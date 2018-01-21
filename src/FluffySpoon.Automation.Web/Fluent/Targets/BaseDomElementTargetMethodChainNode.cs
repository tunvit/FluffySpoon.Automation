﻿using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Fluent.Targets.At;
using FluffySpoon.Automation.Web.Fluent.Targets.From;
using FluffySpoon.Automation.Web.Fluent.Targets.In;
using FluffySpoon.Automation.Web.Fluent.Targets.Of;
using FluffySpoon.Automation.Web.Fluent.Targets.On;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluffySpoon.Automation.Web.Fluent.Find;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Targets
{
	abstract class BaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> :
		BaseMethodChainNode<IBaseMethodChainNode>,
		IBaseDomElementTargetMethodChainNode<TCurrentMethodChainNode, TNextMethodChainNode> 
		where TNextMethodChainNode : IBaseMethodChainNode, new()
		where TCurrentMethodChainNode : IBaseMethodChainNode
	{
		private string _selector;

		protected TNextMethodChainNode Delegate(string selector)
		{
			_selector = selector;
			return Delegate();
		}

		protected TNextMethodChainNode Delegate(IDomElement element)
		{
			return Delegate(new[] { element });
		}

		protected TNextMethodChainNode Delegate(IReadOnlyList<IDomElement> elements)
		{
			Elements = elements;
			return Delegate();
		}

		private TNextMethodChainNode Delegate()
		{
			MethodChainContext.Enqueue(this);
			return MethodChainContext.Enqueue(new TNextMethodChainNode());
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			if(Elements == null) {
				if (_selector == null)
					throw new InvalidOperationException("Elements to target must be found either via a selector or a list of elements.");

				var findNode = new FindMethodChainNode(_selector);
				await findNode.ExecuteAsync(framework);

				Elements = findNode.Elements;
			}

			await base.OnExecuteAsync(framework);
		}

		public TNextMethodChainNode In(string selector) => Delegate(selector);
		public TNextMethodChainNode In(IDomElement element) => Delegate(element);

		public TNextMethodChainNode Of(string selector) => Delegate(selector);
		public TNextMethodChainNode Of(IDomElement element) => Delegate(element);

		public TNextMethodChainNode From(string selector) => Delegate(selector);
		public TNextMethodChainNode From(IDomElement element) => Delegate(element);

		public TNextMethodChainNode On(string selector) => Delegate(selector);
		public TNextMethodChainNode On(IDomElement element) => Delegate(element);

		public TNextMethodChainNode At(string selector) => Delegate(selector);
		public TNextMethodChainNode At(IDomElement element) => Delegate(element);
	}
}