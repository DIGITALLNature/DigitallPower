// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Constants;

public static class XrmMock
{
    internal static class Control
    {
        public static string GridControl => "gridControl";
        public static string MultiSetControl => "multiSet";
        public static string OptionSetControl => "optionSet";
        public static string LookupControl => "lookup";

        public static string NumberControl => "number";

        public static string DateControl => "date";

        public static string StringControl => "string";
        public static string BooleanControl => "boolean";

        public static string QuickViewControl => "quickView";
    }

    internal static class Attributes
    {
        public static string MultiSetAttribute => "multiSet";
        public static string OptionSetAttribute => "optionSet";
        public static string LookupAttribute => "lookup";

        public static string NumberAttribute => "number";

        public static string DateAttribute => "date";

        public static string StringAttribute => "string";
        public static string BooleanAttribute => "boolean";
    }

    internal static class RequiredLevel {
        public static string None => "none";
        public static string Recommended => "recommended";
        public static string Required => "required";
    }
}