﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Announcements.Test.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Announcements.Test.Domain.Resources.Errors", typeof(Errors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The announcement already exist..
        /// </summary>
        internal static string Announcement_Exist {
            get {
                return ResourceManager.GetString("Announcement_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The announcement number must be unique..
        /// </summary>
        internal static string AnnouncementNumber_Unique {
            get {
                return ResourceManager.GetString("AnnouncementNumber_Unique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To update or remove announcements can only admin or owner..
        /// </summary>
        internal static string AnnouncementOwner_Invalid {
            get {
                return ResourceManager.GetString("AnnouncementOwner_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date time has invalid format: {0}. Format must be {1}..
        /// </summary>
        internal static string DateTime_InvalidFormat {
            get {
                return ResourceManager.GetString("DateTime_InvalidFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expiration date must be greater than  the creation date..
        /// </summary>
        internal static string ExpirationDate_Invalid {
            get {
                return ResourceManager.GetString("ExpirationDate_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Еру field {0} is required..
        /// </summary>
        internal static string Field_IsRequired {
            get {
                return ResourceManager.GetString("Field_IsRequired", resourceCulture);
            }
        }
    }
}