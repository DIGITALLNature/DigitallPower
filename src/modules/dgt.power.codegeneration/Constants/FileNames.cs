// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Constants;

public static class FileNames
{
    internal static class DotNet
    {
        public static string Actions => "Actions";
        public static string SdkMessageNames => "SdkMessageNames";
        public static string OptionSetValues => "OptionSetValues";
        public static string Context => "Context";
    }

    internal static class Typescript
    {
        internal static class FileNamePart
        {
            public static string CustomApi => "customapi";
            public static string Entity => "entity";
            public static string EntityRef => "entityref";
            public static string Form => "form";
            public static string Model => "model";
            public static string Odata => "odata";
            public static string Services => "services";
            public static string TestHelper => "mock.form";
            public static string Utils => "utils";
            public static string Webapi => "webapi";
        }

        internal static class FileExtension
        {
            public static string TsExtension => "ts";
            public static string TypeExtension => "d.ts";
        }

        internal static class FileNames
        {
            public static string OptionSetValues => "optionsetvalues";
            public static string SdkMessageNames => "sdkmessagenames";
            public static string TsAuxiliaryExtTypes => "xrm_types_ext";
            public static string XrmMockFormContextBuilder => "xrm_mock_form_test_context_builder";
            public static string XrmMockFormContextTypes => "xrm_mock_form_test_context_types";
            public static string XrmMockFormODataFilter => "xrm_mock_form_odata_filter";
            public static string XrmMockTestTypingsFileName => "xrm_form_tester";
            public static string XrmWebApiTypingsFileName => "xrm_webapi_ext";

        }
    }
}
