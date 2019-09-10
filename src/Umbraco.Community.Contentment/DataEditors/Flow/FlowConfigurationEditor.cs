/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using Umbraco.Core.IO;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class FlowConfigurationEditor : ConfigurationEditor
    {
        private readonly ConfigurationEditorService _service;

        public FlowConfigurationEditor()
            : base()
        {
            _service = new ConfigurationEditorService();

            // TODO: [LK:2019-09-10] Flow will need to be configurable, pre-select which processors are available to the editor, etc.
        }

        public override IDictionary<string, object> ToValueEditor(object configuration)
        {
            var config = base.ToValueEditor(configuration);

            if (config.ContainsKey(ConfigurationEditorConfigurationEditor.Items) == false)
            {
                config[ConfigurationEditorConfigurationEditor.Items] = _service.GetConfigurationEditors<IFlowProcessor>(onlyPublic: true, ignoreFields: false);
            }

            if (config.ContainsKey(ConfigurationEditorConfigurationEditor.OverlayView) == false)
            {
                config.Add(ConfigurationEditorConfigurationEditor.OverlayView, IOHelper.ResolveUrl(ConfigurationEditorDataEditor.DataEditorOverlayViewPath));
            }

            return config;
        }
    }
}
