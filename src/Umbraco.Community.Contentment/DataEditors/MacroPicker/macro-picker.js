/* Copyright © 2019 Lee Kelleher.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

angular.module("umbraco").controller("Umbraco.Community.Contentment.DataEditors.MacroPicker.Controller", [
    "$scope",
    "clipboardService",
    "entityResource",
    "editorService",
    "localizationService",
    "overlayService",
    function ($scope, clipboardService, entityResource, editorService, localizationService, overlayService) {

        console.log("macro-picker.model", $scope.model);

        var defaultConfig = {
            allowCopy: 0,
            allowEdit: 1,
            allowPreview: 0,
            allowRemove: 1,
            availableMacros: [],
            maxItems: 0,
            disableSorting: 0
        };
        var config = angular.extend({}, defaultConfig, $scope.model.config);

        var vm = this;

        function init() {

            $scope.model.value = $scope.model.value || [];

            vm.icon = "icon-settings-alt";
            vm.allowAdd = (config.maxItems === 0 || config.maxItems === "0") || $scope.model.value.length < config.maxItems;
            vm.allowCopy = Object.toBoolean(config.allowCopy) && clipboardService.isSupported();
            vm.allowEdit = Object.toBoolean(config.allowEdit);
            vm.allowPreview = Object.toBoolean(config.allowPreview);
            vm.allowRemove = Object.toBoolean(config.allowRemove);
            vm.published = true;
            vm.sortable = Object.toBoolean(config.disableSorting) === false && (config.maxItems !== 1 && config.maxItems !== "1");

            vm.sortableOptions = {
                axis: "y",
                containment: "parent",
                cursor: "move",
                disabled: vm.sortable === false,
                opacity: 0.7,
                scroll: true,
                tolerance: "pointer",
                stop: function (e, ui) {
                    setDirty();
                }
            };

            vm.add = add;
            vm.copy = copy;
            vm.edit = edit;
            vm.remove = remove;
        };

        function add() {
            var macroPicker = {
                dialogData: {
                    richTextEditor: false,
                    macroData: { macroAlias: "" },
                    allowedMacros: config.availableMacros
                },
                submit: function (model) {

                    $scope.model.value.push({
                        udi: model.selectedMacro.udi,
                        name: model.selectedMacro.name,
                        alias: model.selectedMacro.alias,
                        params: _.object(_.map(model.macroParams, function (p) { return [p.alias, p.value]; }))
                    });

                    if ((config.maxItems !== 0 && config.maxItems !== "0") && $scope.model.value.length >= config.maxItems) {
                        vm.allowAdd = false;
                    }

                    setDirty();

                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            }

            editorService.macroPicker(macroPicker);
        };

        function copy($index) {

            var item = $scope.model.value[$index];

            clipboardService.copy("contentment.macro", item.alias, item);
        };

        function edit($index) {
            var item = $scope.model.value[$index];
            var macroPicker = {
                dialogData: {
                    richTextEditor: false,
                    macroData: { macroAlias: item.alias, macroParamsDictionary: item.params },
                    allowedMacros: config.availableMacros
                },
                submit: function (model) {
                    $scope.model.value[$index] = {
                        udi: model.selectedMacro.udi,
                        name: model.selectedMacro.name,
                        alias: model.selectedMacro.alias,
                        params: _.object(_.map(model.macroParams, function (p) { return [p.alias, p.value]; }))
                    };

                    setDirty();

                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            }

            editorService.macroPicker(macroPicker);
        };

        function remove($index) {
            var keys = ["content_nestedContentDeleteItem", "general_delete", "general_cancel", "contentTypeEditor_yesDelete"];
            localizationService.localizeMany(keys).then(function (data) {
                overlayService.open({
                    title: data[1],
                    content: data[0],
                    closeButtonLabel: data[2],
                    submitButtonLabel: data[3],
                    submitButtonStyle: "danger",
                    submit: function () {

                        $scope.model.value.splice($index, 1);

                        if ((config.maxItems === 0 || config.maxItems === "0") || $scope.model.value.length < config.maxItems) {
                            vm.allowAdd = true;
                        }

                        setDirty();

                        overlayService.close();
                    },
                    close: function () {
                        overlayService.close();
                    }
                });
            });
        };

        function setDirty() {
            if ($scope.propertyForm) {
                $scope.propertyForm.$setDirty();
            }
        };

        init();
    }
]);
