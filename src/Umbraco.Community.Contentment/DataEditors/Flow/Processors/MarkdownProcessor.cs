/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using HeyRed.MarkdownSharp;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class MarkdownProcessor : IFlowProcessor
    {
        public string Name => "Markdown transform";

        public string Description => "Transformers! Markdown in disguise!";

        public string Icon => "icon-wand color-amber";

        public string Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var markdown = new Markdown();
            return markdown.Transform(input);
        }
    }
}
