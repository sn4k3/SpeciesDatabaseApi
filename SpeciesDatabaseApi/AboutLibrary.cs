using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SpeciesDatabaseApi;

public static class AboutLibrary
{
    public static Version Version => ApiAssembly.GetName().Version!;
    public static string VersionStr => Version.ToString(3);
    public static string VersionArch => $"{VersionStr} {RuntimeInformation.ProcessArchitecture}";
    public static string SoftwareWithVersion => $"{AssemblyName} v{VersionStr}";
    public static string SoftwareWithVersionArch => $"{AssemblyName} v{VersionArch}";

    #region Assembly properties
    public static Assembly ApiAssembly => Assembly.GetExecutingAssembly();

    public static string AssemblyVersion => ApiAssembly.GetName().Version?.ToString()!;

    public static string AssemblyName => ApiAssembly.GetName().Name!;

    public static string AssemblyTitle
    {
        get
        {
            var attributes = ApiAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != string.Empty)
                {
                    return titleAttribute.Title;
                }
            }
            return Path.GetFileNameWithoutExtension(ApiAssembly.Location);
        }
    }

    public static string AssemblyDescription
    {
        get
        {
            var attributes = ApiAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyDescriptionAttribute)attributes[0]).Description;

        }
    }

    public static string AssemblyProduct
    {
        get
        {
            var attributes = ApiAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    public static string AssemblyCopyright
    {
        get
        {
            var attributes = ApiAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    public static string AssemblyCompany
    {
        get
        {
            var attributes = ApiAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }
    #endregion
}