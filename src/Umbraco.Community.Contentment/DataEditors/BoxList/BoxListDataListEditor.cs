/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using Umbraco.Core.IO;

namespace Umbraco.Community.Contentment.DataEditors
{
#if !DEBUG
    // TODO: IsWorkInProgress - Under development.
    [global::Umbraco.Core.Composing.HideFromTypeFinder]
#endif
    internal class BoxListDataListEditor : IDataListEditor
    {
        public string Name => "Box List";

        public string Description => "Select values from a list of box options.";

        public string Icon => "icon-brick";

        public Dictionary<string, object> DefaultConfig => default;

        public string View => IOHelper.ResolveUrl(Constants.Internals.EditorsPathRoot + "box-list.html");
    }
}
