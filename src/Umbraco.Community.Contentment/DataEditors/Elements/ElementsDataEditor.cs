/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;

namespace Umbraco.Community.Contentment.DataEditors
{
    [DataEditor(
        DataEditorAlias,
        EditorType.PropertyValue,
        DataEditorName,
        DataEditorViewPath,
        ValueType = ValueTypes.Json,
        Group = Core.Constants.PropertyEditors.Groups.RichContent,
#if DEBUG
        Icon = "icon-block color-red"
#else
        Icon = DataEditorIcon
#endif
        )]
    internal sealed class ElementsDataEditor : DataEditor
    {
        internal const string DataEditorAlias = Constants.Internals.DataEditorAliasPrefix + "Elements";
        internal const string DataEditorName = Constants.Internals.DataEditorNamePrefix + "Elements";
        internal const string DataEditorViewPath = Constants.Internals.EditorsPathRoot + "_empty.html";
        internal const string DataEditorListViewPath = Constants.Internals.EditorsPathRoot + "elements-list.html";
        internal const string DataEditorBlocksViewPath = Constants.Internals.EditorsPathRoot + "elements-blocks.html";
        internal const string DataEditorOverlayViewPath = Constants.Internals.EditorsPathRoot + "elements.overlay.html";
        internal const string DataEditorIcon = "icon-item-arrangement";

        private readonly IContentService _contentService;
        private readonly IContentTypeService _contentTypeService;
        private readonly IdkMap _idkMap;

        public ElementsDataEditor(ILogger logger, IContentService contentService, IContentTypeService contentTypeService, IdkMap idkMap)
            : base(logger)
        {
            _contentService = contentService;
            _contentTypeService = contentTypeService;
            _idkMap = idkMap;
        }

        protected override IConfigurationEditor CreateConfigurationEditor() => new ElementsConfigurationEditor(_contentService, _contentTypeService, _idkMap);

        protected override IDataValueEditor CreateValueEditor() => new ElementsDataValueEditor(Attribute);
    }
}
