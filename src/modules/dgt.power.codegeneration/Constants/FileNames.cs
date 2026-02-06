// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Constants;

public static class FileNames
{
    public static class DotNet
    {
        public static string Actions => "Actions";
        public static string SdkMessageNames => "SdkMessageNames";
        public static string OptionSetValues => "OptionSetValues";
        public static string Context => "Context";
    }

    public static class Typescript
    {
        public static string CustomApi => "customapi";
        public static string Entity => "entity";
        public static string EntityRef => "entityref";
        public static string Form => "form";
        public static string Model => "model";
        public static string Odata => "odata";
        public static string OptionSetValues => "optionsetvalues";
        public static string SdkMessageNames => "sdkmessagenames";
        public static string Services => "services";
        public static string Utils => "utils";
        public static string Webapi => "webapi";

        public static string TestHelper => "mock.form";

        public static string TsTypeExtension => "d.ts";

        public static string TsExtension => "ts";

        public static string XrmWebApiTypingsFileName => "xrm_webapi_ext";
        public static string XrmMockTypingsFileName => "xrm_mock_form";

        public static string XrmMockFormContextBuilder => "xrm_mock_form_test_context_builder";

        public static string XrmMockFormContextTypes => "xrm_mock_form_test_context_types";

    }
}
