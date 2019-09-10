/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class YourNameProcessor : IFlowProcessor
    {
        public string Name => "Your name";

        public string Description => "This is who you are.";

        public string Icon => "icon-name-badge color-light-blue";

        [ConfigurationField("yourName", "What is your name?", "textstring", Description = "Enter your name, that's who you are.")]
        public string YourName { get; set; }

        public string Process(string input)
        {
            return $"Hello {YourName}.";
        }
    }
}
