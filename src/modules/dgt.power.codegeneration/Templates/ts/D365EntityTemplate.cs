﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace dgt.power.codegeneration.Templates.ts
{
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class D365EntityTemplate : D365EntityTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("/* eslint-disable */\r\n///<reference path=\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(TypingPath));
            this.Write("\" />\r\n\r\nimport { D365Model } from \"./model\";\r\n\r\nexport module D365");
            this.Write(this.ToStringHelper.ToStringWithCulture(CamelCase(EntityMetadata.SchemaName)));
            this.Write("Entity {\r\n\texport class Attributes {\r\n\t\tconstructor() {\r\n\t\t}\r\n");
 foreach(var attr in Filter(EntityMetadata.Attributes))
{ 
		var attrName = Unique(CamelCase(Sanitize(attr.SchemaName)),"A"+EntityMetadata.LogicalName);

            this.Write("\t\tpublic static ");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrName));
            this.Write(": string = \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\";\r\n");
 
} // End Attribute loop

            this.Write("\t}\r\n\r\n\t");
            this.Write(this.ToStringHelper.ToStringWithCulture(Summary(GetLocalizedLabel(EntityMetadata.Description),1)));
            this.Write("\r\n\texport class Entity implements D365Model.Entity {\r\n\r\n\t\tpublic static Entitylog" +
                    "icalName: string = \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(EntityMetadata.LogicalName));
            this.Write(@""";

		public FormContext!: Xrm.FormContext;

		constructor(private context?: Xrm.FormContext) {
			if (this.context) {
				this.FormContext = this.context;
			}
		}

		EntitylogicalName(): string {
			return Entity.EntitylogicalName;
		};

		EntityId(): string {
			return this.FormContext.data.entity.getId().replace(""{"", """").replace(""}"", """").trim();
		};

");
 foreach(var attr in Filter(EntityMetadata.Attributes))
{ 
		var attrName = Unique(CamelCase(Sanitize(attr.SchemaName)),"B"+EntityMetadata.LogicalName);
		var attrType = TSTypeSet.ConvertType(attr.AttributeType, attr.LogicalName);

            this.Write("\t\t");
            this.Write(this.ToStringHelper.ToStringWithCulture(Summary(GetLocalizedLabel(attr.Description),2)));
            this.Write("\r\n\t\t");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrName));
            this.Write(": D365Model.EntityField<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Attribute));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write("> = new D365Model.EntityField<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Attribute));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write(">(\r\n\t\t\t//D365");
            this.Write(this.ToStringHelper.ToStringWithCulture(CamelCase(EntityMetadata.SchemaName)));
            this.Write("Entity.Attributes.");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrName));
            this.Write(",\r\n\t\t\t\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\",\r\n\t\t\t");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.XrmEnum));
            this.Write(",\r\n\t\t\t() => this._a<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Attribute));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\"),\r\n\t\t\t(header?: boolean, index?: number) => this._c<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\", header, index),\r\n\t\t\t(delegate: (control: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write(") => void) => this._f<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\", delegate),\r\n\t\t\t(value) => this._v<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\", value),\r\n\t\t\t(value) => this._d<");
            this.Write(this.ToStringHelper.ToStringWithCulture(attrType.Control));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(attr.LogicalName));
            this.Write("\", value)\r\n\t\t);\r\n\r\n");
 
} // End Attribute loop

            this.Write(@"		private _a<A extends Xrm.Attributes.Attribute>(attributeName: string): A {
			return this.FormContext.getAttribute(attributeName);
		}

		private _c<C extends Xrm.Controls.StandardControl>(attributeName: string, header?: boolean, index?: number): C {
			var name = attributeName;
			if (header) {
				name = ""header_process_"" + name;
			}
			if (index) {
				name = name + ""_"" + index;
			}
			return this.FormContext.getControl(name);
		}

		private _v<C extends Xrm.Controls.StandardControl>(attributeName: string, value: boolean) {
			this.FormContext.getAttribute(attributeName)?.controls.forEach((c) => (c as C).setVisible(value));
		}

		private _d<C extends Xrm.Controls.StandardControl>(attributeName: string, value: boolean) {
			this.FormContext.getAttribute(attributeName)?.controls.forEach((c) => (c as C).setDisabled(value));
		}

		private _f<C extends Xrm.Controls.StandardControl>(attributeName: string, callback: (control: C) => void) {
			this.FormContext.getAttribute(attributeName)?.controls.forEach((c) => callback((c as C)));
		}
	}

");
 if(!SuppressOptions)
{ // Start Options

            this.Write("\texport module Optionsets {\r\n");
 	foreach(var optionField in FilterOptions(EntityMetadata.Attributes)) 
	{ 

            this.Write("\t\texport class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Unique(CamelCase(Sanitize(optionField.SchemaName)),"O"+EntityMetadata.LogicalName)));
            this.Write(" implements D365Model.Optionset {\r\n\t\t\tconstructor() {\r\n\t\t\t}\r\n\r\n");
 			foreach(var option in optionField.Options)	
			{ 
            this.Write("\t\t\tpublic static ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Unique(CamelCase(Sanitize(option.Label)),EntityMetadata.LogicalName+optionField.SchemaName)));
            this.Write(": D365Model.OptionsetValue = new D365Model.OptionsetValue(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(Sanitize(option.Label, true, true, true)));
            this.Write("\", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(option.Value));
            this.Write(");\r\n");
 			} 
            this.Write("\t\t}\r\n");
	
	} // End Optionset loop

            this.Write("\t}\r\n");
 
} // End Options

            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class D365EntityTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
