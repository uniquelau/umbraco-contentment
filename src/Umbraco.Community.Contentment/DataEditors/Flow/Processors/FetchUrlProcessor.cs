/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Net;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class FetchUrlProcessor : IFlowProcessor
    {
        public string Name => "Fetch URL";

        public string Description => "Go get 'um tiger!";

        public string Icon => "icon-globe color-green";

        [ConfigurationField("url", "URL", "textstring", Description = "Enter the URL to fetch from the web.")]
        public string Url { get; set; }

        public string Process(string input)
        {
            if (string.IsNullOrWhiteSpace(Url))
                return input;

            using (var client = new WebClient())
            {
                return client.DownloadString(Url);
            }
        }
    }
}
