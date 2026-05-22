namespace PetraConectBack.RecursosPetra.RsSQL {
    using System;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "18.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class RsFactura {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;

        internal RsFactura() {
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PetraConectBack.RecursosPetra.RsSQL.RsFactura", typeof(RsFactura).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        public static string GetFactura {
            get {
                return ResourceManager.GetString("GetFactura", resourceCulture);
            }
        }

        public static string GetFacturasByStatusActual {
            get {
                return ResourceManager.GetString("GetFacturasByStatusActual", resourceCulture);
            }
        }

        public static string SetFactura {
            get {
                return ResourceManager.GetString("SetFactura", resourceCulture);
            }
        }
    }
}
