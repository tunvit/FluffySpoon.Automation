﻿using FluffySpoon.Automation.Web.Dom;
using FluffySpoon.Automation.Web.Exceptions;
using FluffySpoon.Automation.Web.Fluent.Expect.Root;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web.Fluent.Expect.Value
{
	class ExpectValueInTargetsMethodChainNode : ExpectMethodChainRoot<ExpectValueMethodChainNode>, IExpectValueInTargetsMethodChainNode
	{
		public override IReadOnlyList<IDomElement> Elements
		{
			get => Parent?.Elements;
		}

		protected override async Task OnExecuteAsync(IWebAutomationFrameworkInstance framework)
		{
			if(!Elements.Any(x => x.Value == Parent.Value))
				throw ExpectationNotMetException.FromMethodChainNode(this, "Expected value \"" + Parent.Value + "\" to be found in all of the matched elements.");

			await base.OnExecuteAsync(framework);
		}
	}
}