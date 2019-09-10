/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.IO;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class StringReplaceProcessor : IFlowProcessor
    {
        public string Name => "String replace";

        public string Description => "Re-re-re-replace me baby!";

        public string Icon => "icon-repeat color-orange";

        [ConfigurationField(typeof(TokensConfigurationField))]
        public IEnumerable<DataListItem> Tokens { get; set; }

        public string Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || Tokens == null || Tokens.Any() == false)
                return input;

            var result = input;

            foreach (var token in Tokens)
            {
                result = result.Replace(token.Name, token.Value);
            }

            return result;
        }

        class TokensConfigurationField : ConfigurationField
        {
            public const string Tokens = "tokens";

            public TokensConfigurationField()
                : base()
            {
                var listFields = new[]
                {
                    new ConfigurationField
                    {
                        Key = "name",
                        Name = "Old",
                        View = "textstring"
                    },
                    new ConfigurationField
                    {
                        Key = "value",
                        Name = "New",
                        View = "textstring"
                    }
                };

                Key = Tokens;
                Name = nameof(Tokens);
                Description = "Enter the tokens to be replaced, the old with the new.";
                View = IOHelper.ResolveUrl(DataTableDataEditor.DataEditorViewPath);
                Config = new Dictionary<string, object>()
                {
                    { DataTableConfigurationEditor.FieldItems, listFields },
                    { MaxItemsConfigurationField.MaxItems, 0 },
                    { DisableSortingConfigurationField.DisableSorting, Constants.Values.False },
                    { DataTableConfigurationEditor.RestrictWidth, Constants.Values.True },
                    { DataTableConfigurationEditor.UsePrevalueEditors, Constants.Values.True }
                };
            }
        }
    }
}
