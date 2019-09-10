/* Copyright © 2019 Lee Kelleher, Umbrella Inc and other contributors.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Serialization;

namespace Umbraco.Community.Contentment.DataEditors
{
    public class FlowValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(IPublishedPropertyType propertyType) => propertyType.EditorAlias.InvariantEquals(FlowDataEditor.DataEditorAlias);

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) => PropertyCacheLevel.None;

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(string);

        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview)
        {
            if (source is string value)
            {
                var array = JArray.Parse(value);
                var items = new List<IFlowProcessor>();

                foreach (var item in array)
                {
                    var type = TypeFinder.GetTypeByName(item.Value<string>("type"));

                    if (type != null)
                    {
                        var serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
                        {
                            ContractResolver = new ConfigurationFieldContractResolver(),
                            Converters = new List<JsonConverter>(new[] { new FuzzyBooleanConverter() })
                        });

                        if (item["value"].ToObject(type, serializer) is IFlowProcessor obj)
                        {
                            items.Add(obj);
                        }
                    }
                }

                return items;
            }

            return base.ConvertSourceToIntermediate(owner, propertyType, source, preview);
        }

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            if (inter is List<IFlowProcessor> processors)
            {
                var result = default(string);

                foreach (var processor in processors)
                {
                    result = processor.Process(result);
                }

                return result;
            }

            return base.ConvertIntermediateToObject(owner, propertyType, referenceCacheLevel, inter, preview);
        }
    }
}
