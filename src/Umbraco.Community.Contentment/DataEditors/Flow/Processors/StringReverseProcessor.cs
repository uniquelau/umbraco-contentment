/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Linq;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class StringReverseProcessor : IFlowProcessor
    {
        public string Name => "String reversal";

        public string Description => "Flip ya', flip ya' for real.";

        public string Icon => "icon-traffic color-purple";

        [ConfigurationField("reverse", "Reverse?", "boolean", Description = "Enable to put the input in reverse.")]
        public bool Reverse { get; set; }

        public string Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input) == false && Reverse)
                return new string(input.ToCharArray().Reverse().ToArray());

            return input;
        }
    }
}
