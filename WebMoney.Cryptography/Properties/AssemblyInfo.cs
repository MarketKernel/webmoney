using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]

//[assembly: AllowPartiallyTrustedCallers]

[assembly: AssemblyTitle("WebMoney.Cryptography")]
[assembly: AssemblyDescription("See: www.wmsigner.com")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("WMTransfer Ltd.")]
[assembly: AssemblyProduct("WebMoney API for .Net Framework")]
[assembly: AssemblyCopyright("© Dmitry Kukushkin <support@wmsigner.com> 2007-2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("862a440d-b4c1-4f81-828a-7faa781fd5c7")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.3.1.0")]
[assembly: AssemblyFileVersion("3.3.1.0")]
