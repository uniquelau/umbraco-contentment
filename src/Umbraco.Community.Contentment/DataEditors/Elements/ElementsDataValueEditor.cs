/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using Umbraco.Core.IO;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class ElementsDataValueEditor : HideLabelDataValueEditor
    {
        public ElementsDataValueEditor(DataEditorAttribute attribute)
            : base(attribute)
        { }

        public override object Configuration
        {
            get => base.Configuration;
            set
            {
                base.Configuration = value;

                // NOTE: I'd have preferred to do this in `ElementConfigurationEditor.ToValueEditor`, but I couldn't alter the `View` from there.
                // ...and this method is triggered before `ToValueEditor`, and there's nowhere else I can manipulate the configuration values. [LK]
                if (value is Dictionary<string, object> config &&
                    config.TryGetValue(ElementsConfigurationEditor.ElementsDisplayModeConfigurationField.DisplayMode, out var displayMode) &&
                    displayMode is string view)
                {
                    View = IOHelper.ResolveUrl(view);
                }
            }
        }

        // TODO: [LK:2019-09-27] Might need to look at how NestedContent handles the `ConvertDbToString` and `ToEditor` and `FromEditor` recursive calls.
        // https://github.com/umbraco/Umbraco-CMS/blob/release-8.1.5/src/Umbraco.Web/PropertyEditors/NestedContentPropertyEditor.cs
    }
}
