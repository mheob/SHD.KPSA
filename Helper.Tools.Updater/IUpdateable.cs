namespace HelperTools.Updater
{
    using System;

    /// <summary>The interface that all applications need to implement in order to use SharpUpdate</summary>
    public interface IUpdateable
    {
        /// <summary>The name of your application as you want it displayed on the update form</summary>
        string ApplicationName { get; }

        /// <summary>An identifier string to use to identify your application in the update.json.</summary>
        /// <remarks>Should be the same as your appId in the update.json.</remarks>
        string ApplicationId { get; }

        ///// <summary>The current assembly</summary>
        //Assembly ApplicationAssembly { get; }

        ///// <summary>The application's icon to be displayed in the top left</summary>
        //Icon ApplicationIcon { get; }

        /// <summary>The location of the update.json on a server</summary>
        Uri UpdateJsonLocation { get; }

        ///// <summary>
        ///// The context of the program. For Windows Forms Applications, use 'this' Console Apps, reference System.Windows.Forms and
        ///// return null.
        ///// </summary>
        //Form Context { get; }
    }
}