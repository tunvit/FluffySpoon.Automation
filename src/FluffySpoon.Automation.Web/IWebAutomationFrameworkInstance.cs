﻿using FluffySpoon.Automation.Web.Dom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluffySpoon.Automation.Web
{
	public interface IWebAutomationFrameworkInstance: IDisposable
	{
		Task<IReadOnlyList<IDomElement>> EvaluateJavaScriptAsDomElementsAsync(string code);
        Task<string> EvaluateJavaScriptAsync(string code);

        Task OpenAsync(string uri);
        Task EnterTextInAsync(string text, string selector);
    }
}