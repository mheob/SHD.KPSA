﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SHD.KPSA.Utils.Properties {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SHD.KPSA.Utils.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die There has been an error: {0} ähnelt.
        /// </summary>
        public static string ExceptionDialogDefaultText {
            get {
                return ResourceManager.GetString("ExceptionDialogDefaultText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Error! ähnelt.
        /// </summary>
        public static string ExceptionDialogDefaultTitle {
            get {
                return ResourceManager.GetString("ExceptionDialogDefaultTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Error reading file: {0} ähnelt.
        /// </summary>
        public static string ExceptionDialogFileAccessText {
            get {
                return ResourceManager.GetString("ExceptionDialogFileAccessText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Error accessing file ähnelt.
        /// </summary>
        public static string ExceptionDialogFileAccessTitle {
            get {
                return ResourceManager.GetString("ExceptionDialogFileAccessTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Nothing has been selected for processing. ähnelt.
        /// </summary>
        public static string NothingToDoDialogText {
            get {
                return ResourceManager.GetString("NothingToDoDialogText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Nothing selected for processing! ähnelt.
        /// </summary>
        public static string NothingToDoDialogTitle {
            get {
                return ResourceManager.GetString("NothingToDoDialogTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Please select the desired folder. ähnelt.
        /// </summary>
        public static string OpenFolderDescription {
            get {
                return ResourceManager.GetString("OpenFolderDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The selected data is currently being processed ... ähnelt.
        /// </summary>
        public static string ProgressDialogText {
            get {
                return ResourceManager.GetString("ProgressDialogText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Data are processed ähnelt.
        /// </summary>
        public static string ProgressDialogTitle {
            get {
                return ResourceManager.GetString("ProgressDialogTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The processing of {0} files is completed. {1}{1} Should the files displaying? ähnelt.
        /// </summary>
        public static string ProgressFinishedDialogText {
            get {
                return ResourceManager.GetString("ProgressFinishedDialogText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Process completed ähnelt.
        /// </summary>
        public static string ProgressFinishedDialogTitle {
            get {
                return ResourceManager.GetString("ProgressFinishedDialogTitle", resourceCulture);
            }
        }
    }
}