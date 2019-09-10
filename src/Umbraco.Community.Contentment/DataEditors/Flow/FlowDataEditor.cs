/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    [DataEditor(
        DataEditorAlias,
#if DEBUG
        EditorType.PropertyValue, // NOTE: IsWorkInProgress [LK]
#else
        EditorType.Nothing,
#endif
        DataEditorName,
        DataEditorViewPath,
        ValueType = ValueTypes.Json,
        Group = Constants.Conventions.PropertyGroups.ZeroCode,
#if DEBUG
        Icon = "icon-block color-red"
#else
        Icon = DataEditorIcon
#endif
        )]
    public class FlowDataEditor : DataEditor
    {
        internal const string DataEditorAlias = Constants.Internals.DataEditorAliasPrefix + "Flow";
        internal const string DataEditorName = Constants.Internals.DataEditorNamePrefix + "Flow";
        internal const string DataEditorViewPath = Constants.Internals.EditorsPathRoot + "flow.html";
        internal const string DataEditorIcon = "icon-flash color-red";

        public FlowDataEditor(ILogger logger)
            : base(logger)
        { }

        // IDEAS!
        // https://github.com/deanebarker/Denina-Sharp/wiki/Use-Cases

        protected override IConfigurationEditor CreateConfigurationEditor() => new FlowConfigurationEditor();
    }
}
