﻿//------------------------------------------------------------------------------
// <copyright file="ChangeBrowserControlPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using VSGerrit.Api.Common.Settings;

namespace VSGerrit.Features.ChangeBrowser
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(ChangeBrowserToolWindow))]
    [Guid(ChangeBrowserPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ChangeBrowserPackage : Package
    {
        /// <summary>
        /// ChangeBrowserControlPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "c3bedfe3-6b53-4dd7-a786-949077ab37e0";

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeBrowserToolWindow"/> class.
        /// </summary>
        public ChangeBrowserPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            ChangeBrowserCommand.Initialize(this);
            ConfigurationProvider.Instance.Initialize(new GerritConfiguration("czaharia", "e0QPJ3wSivllpa4MtJD/0nfGkrwqGI/4pgic3+/B+g", "http://gerrit.ullink.lan:8080/a/"));

            var componentModel = (IComponentModel) GetService(typeof (SComponentModel));
            var workspace = componentModel.GetService<VisualStudioWorkspace>();

            

            base.Initialize();
        }

        #endregion
    }
}
